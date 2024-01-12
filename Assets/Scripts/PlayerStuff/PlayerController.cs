 using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public enum Invulnerability
{
    Damagable,
    NoDamagable,
}
public class PlayerController : Character
{
    private AudioSource _aS;
    public ScriptableState Walk, DashAction;
    [SerializeField]
    private PlayerData _playerDataSO;
    [SerializeField]
    private PlayerStats _playerStatsSO;
    private PlayerData _clonePlayerDataSO;
    public PlayerData PlayerDataSO
    {
        get { return _clonePlayerDataSO;}
        set { _clonePlayerDataSO = value;}
    }
    public IWeaponControler Weapon { set => _weapon = value; }
    private IWeaponControler _weapon;
    private Rigidbody2D _rb;
    private Animator _anim;
    private ControlStats _controlStats;
    private ApplyModificator _applyModificator;
    // Start is called before the first frame update
    void Awake()
    {
        _clonePlayerDataSO = Instantiate(_playerDataSO);
        _clonePlayerDataSO.Damagable = Invulnerability.Damagable;
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _clonePlayerDataSO.life = _clonePlayerDataSO.maxlife;
        _clonePlayerDataSO.State = Life.Alive;
        _anim = gameObject.GetComponent<Animator>();
        _controlStats = gameObject.GetComponent<ControlStats>();
        _applyModificator = GetComponent<ApplyModificator>();
        stop = Stop;
     }
    // Update is called once per frame
   protected override void Update()
    {
        base.Update();
        ChangeSpriteByRotation();
    }

    private void ChangeSpriteByRotation()
    {
        Vector3 vector = gameObject.GetComponentInParent<PlayerController>().VectorMousePlayerAngle();
        float angle = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
        float xPosition = Mathf.Cos(Mathf.Deg2Rad * angle);
        float yPosition = Mathf.Sin(Mathf.Deg2Rad * angle);
        _anim.SetFloat("MPositionX", xPosition);
        _anim.SetFloat("MPositionY", yPosition);
        _anim.SetFloat("Life", _clonePlayerDataSO.life);
    }
    public override void MovementBehaivour(float directionX, float directionY)
    {
        ScriptableMovement movement;
        if (currentState == Walk)
        {
            movement = (ScriptableMovement)currentState.Action;
            movement.directionX = directionX;
            movement.directionY = directionY;
            movement.speed = _clonePlayerDataSO.speed;
            movement.rb = _rb;
            _clonePlayerDataSO.Damagable = Invulnerability.Damagable;
        }
    }
    public Vector3 VectorMousePlayerAngle()
    {
       return Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    }
    public override void TakeDamage(float damage)
    {
        if (_clonePlayerDataSO.Damagable == Invulnerability.Damagable)
        {
            gameObject.GetComponent<LifeControler>().ModifyLife(damage * -1, ref _clonePlayerDataSO.life, _clonePlayerDataSO.maxlife);
            _anim.SetBool("Damage", true);
            _clonePlayerDataSO.Damagable = Invulnerability.NoDamagable;
            StopMomentum();
            StartCoroutine(InvulnerabilityTime(0.5f, "Damage"));
        }
    }
    public void SumLife(float extraLife)
    {
        gameObject.GetComponent<LifeControler>().ModifyLife(extraLife, ref _clonePlayerDataSO.life, _clonePlayerDataSO.maxlife);
    }

    public void MeleeAttack()
    {
        if (_weapon != null)
        {
            _applyModificator.UpdateWeaponStats(_weapon);
            _weapon?.FirstButtonAttack();
        }
    }
    public void Shoot()
    {
        if (_weapon != null)
        {
            _applyModificator.UpdateWeaponStats(_weapon);
            _weapon?.SecondButtonAttack();
        }
    }
    public override void OnDeath()
    {
        _clonePlayerDataSO.State = Life.Death;
    }
    public void ThrowBombs()
    {
        gameObject.GetComponent<GestionInventory>().ThrowBombs();
    }
    public void Dash()
    {
        var dash = (ScriptableDash)DashAction.Action;
        dash.dashSpeed = _clonePlayerDataSO.dashSpeed;
        dash.controlStats = _controlStats;
        dash.dashDuration= _clonePlayerDataSO.dashDuration;
        dash.position = transform.position;
        dash.rb = _rb;
        StateTransitor(DashAction);
        _anim.SetBool("Dash", true);
        _clonePlayerDataSO.Damagable = Invulnerability.NoDamagable;
        StartCoroutine(InvulnerabilityTime(_clonePlayerDataSO.dashDuration, "Dash"));
    }
    private IEnumerator InvulnerabilityTime(float time, string animation)
    {
        yield return new WaitForSeconds(time);
        _anim.SetBool(animation, false);
        _clonePlayerDataSO.Damagable = Invulnerability.Damagable;
        StateTransitor(Walk);
    }
}
