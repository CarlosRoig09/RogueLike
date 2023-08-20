using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Turret : Enemy
{
   //private ShootControler _shootControler;
    [SerializeField]
    private ShootSO _shootSO;
    private ShootSO _cloneShootSO;
    private ShootControler _shootControler;
    public ScriptableState Shoot, Teleport;
    private ScriptableState _shoot, _teleport;
    [SerializeField]
    private LayerMask _playerLayerMask;
    private bool _reloading;
    protected override void Start()
    {
        base.Start();
        _shootControler = gameObject.GetComponent<ShootControler>();
        _cloneShootSO = Instantiate(_shootSO);
      _shootControler.NewWeapon(_cloneShootSO);
        //var shoot = (ScriptableShoot)_shoot.Action;
        //shoot.ShootControler = _shootControler;
        currentState = _shoot;
        _reloading = false;
    }
    protected override void Update()
    {
        base.Update();
        if (_cloneShootSO.currentBullets == 0&&!_reloading)
        {
            var teleport = (ScriptableTeleport)_teleport.Action;
            Teleported(teleport,RandomPositions());
            teleport.characterTransform = transform;
            StateTransitor(_teleport);
            _reloading = true;
        }
        else if (_reloading)
        {
            StateTransitor(_shoot);
            if(_cloneShootSO.currentBullets==_cloneShootSO.TotalBullets)
            {
                _reloading= false;
            }
        }
    }

    private bool Teleported(ScriptableTeleport teleport, Vector3[] PosiblePositions)
    {
        foreach (var position in PosiblePositions)
        {
            if ((_player.transform.position - position).magnitude > 3)
            {
                teleport.newPosition = position;
                return true;
            }
        }
       teleport.newPosition = PosiblePositions[Random.Range(0,PosiblePositions.Length)];
        return false;
    }

    private Vector3[] RandomPositions()
    {
        Vector3[] randomPositions = (Vector3[])EnemyWaveControler.Instance.AllPositions.Clone();
        for (int i = 0; i < randomPositions.Length;)
        {
            int y;
            int x;
            if ((x = Random.Range(0, randomPositions.Length)) != (y = Random.Range(0, randomPositions.Length)))
            {
                i++;
                (randomPositions[x], randomPositions[y]) = (randomPositions[y], randomPositions[x]);
            }
        }
        return randomPositions;
    }
}
