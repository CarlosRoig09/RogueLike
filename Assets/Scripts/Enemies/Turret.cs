using System.Collections;
using System.Collections.Generic;
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
    private bool _firstShootAfterTeleporting;
    protected override void Start()
    {
        base.Start();
        _shootControler = gameObject.GetComponent<ShootControler>();
        _cloneShootSO = Instantiate(_shootSO);
      _shootControler.NewWeapon(_cloneShootSO);
        _shoot = ScriptableStateMethods.CopyAStateMachineState(Shoot, new List<ScriptableState>());
        _teleport = ScriptableStateMethods.ReturnStateWithId(_shoot.ScriptableStateTransitor,Teleport.Id);
        stop = ScriptableStateMethods.ReturnStateWithId(_shoot.ScriptableStateTransitor, Stop.Id);
        var shoot = (ScriptableShoot)_shoot.Action;
        shoot.ShootControler = _shootControler;
        currentState = _shoot;
        _reloading = false;
        _firstShootAfterTeleporting = false;
    }
    protected override void Update()
    {
        base.Update();
            if (_cloneShootSO.currentBullets == 0 && !_reloading)
            {
             cloneEnemyData.Damagable = Invulnerability.NoDamagable;
            GetComponent<Animator>().SetBool("Teleport", true);
            }
            else if (_reloading)
            {
            if (_firstShootAfterTeleporting)
            {
                _firstShootAfterTeleporting = false;
                StateTransitor(_shoot);
            }
                if (_cloneShootSO.currentBullets == _cloneShootSO.TotalBullets)
                {
                    _reloading = false;
                }
            }
    }

    private bool Teleported(ScriptableTeleport teleport, List<Vector3> PosiblePositions)
    {
        foreach (var position in PosiblePositions)
        {
            if ((_player.transform.position - position).magnitude > 3)
            {
                teleport.newPosition = position;
                return true;
            }
        }
       teleport.newPosition = PosiblePositions[Random.Range(0,PosiblePositions.Count)];
        return false;
    }

    public void TeleportAction()
    {
        GetComponent<Animator>().SetBool("Teleport", false);
        var teleport = (ScriptableTeleport)_teleport.Action;
        Teleported(teleport, RandomPositions());
        teleport.characterTransform = transform;
        StateTransitor(_teleport);
        _reloading = true;
        _firstShootAfterTeleporting = true;
        cloneEnemyData.Damagable = Invulnerability.Damagable;
    }

    private List<Vector3> RandomPositions()
    {
        List<Vector3> randomPositions = new (EnemyWaveControler.Instance.AllPositions);
        for (int i = 0; i < randomPositions.Count;)
        {
            int y;
            int x;
            if ((x = Random.Range(0, randomPositions.Count)) != (y = Random.Range(0, randomPositions.Count)))
            {
                i++;
                (randomPositions[x], randomPositions[y]) = (randomPositions[y], randomPositions[x]);
            }
        }
        return randomPositions;
    }
}
