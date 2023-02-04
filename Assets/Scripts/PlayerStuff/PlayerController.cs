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
    public ScriptableState Walk, DashAction;
    public PlayerData _playerDataSO;
    [SerializeField]
    private PlayerStats _playerStatsSO;
    public IWeaponControler Weapon { set => _weapon = value; }
    private IWeaponControler _weapon;
    private Rigidbody2D _rb;
    private Animator _anim;
    private ControlStats _controlStats;
    // Start is called before the first frame update
    void Start()
    {
        _playerDataSO.Damagable = Invulnerability.Damagable;
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _playerDataSO.life = _playerDataSO.maxlife;
        _playerDataSO.State = Life.Alive;
        _anim = gameObject.GetComponent<Animator>();
        _controlStats = gameObject.GetComponent<ControlStats>();
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
        _anim.SetFloat("Movement", Mathf.Abs(_rb.velocity.x) + Mathf.Abs(_rb.velocity.y));
        _anim.SetFloat("MPositionX", xPosition);
        _anim.SetFloat("MPositionY", yPosition);
        _anim.SetFloat("Life", _playerDataSO.life);
    }
    public override void Movement(float directionX, float directionY)
    {
        ScriptableMovement movement;
        if (currentState == Walk)
        {
            movement = (ScriptableMovement)currentState.Action;
            movement.directionX = directionX;
            movement.directionY = directionY;
            movement.speed = _playerDataSO.speed;
            movement.rb = _rb;
            _playerDataSO.Damagable = Invulnerability.Damagable;
        }
    }
    public Vector3 VectorMousePlayerAngle()
    {
       return Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    }
    public override void TakeDamage(float damage)
    {
        if (_playerDataSO.Damagable == Invulnerability.Damagable)
        {
            gameObject.GetComponent<LifeControler>().ModifyLife(damage * -1, ref _playerDataSO.life, _playerDataSO.maxlife);
            _anim.SetBool("Damage", true);
            _playerDataSO.Damagable = Invulnerability.NoDamagable;
            _rb.velocity = new Vector3(0, 0);
            StartCoroutine(InvulnerabilityTime(0.5f, "Damage"));
        }
    }
    public void SumLife(float extraLife)
    {
        gameObject.GetComponent<LifeControler>().ModifyLife(extraLife, ref _playerDataSO.life, _playerDataSO.maxlife);
    }

    public void MeleeAttack()
    {
        if (_weapon != null)
        {
            _weapon.WeaponDamage = _playerStatsSO.Damage;
            _weapon.WeaponSpeed = _playerStatsSO.Speed;
            _weapon?.FirstButtonAttack();
        }
    }
    public void Shoot()
    {
        if (_weapon != null)
        {
            _weapon.ProyectileDamage = _playerStatsSO.ProyectileDamage;
            _weapon.ProyectileSpeed = _playerStatsSO.ProyectileSpeed;
            _weapon?.SecondButtonAttack();
        }
    }
    public override void OnDeath()
    {
        _playerDataSO.State = Life.Death;
    }
    public void Dash()
    {
        var dash = (ScriptableDash)DashAction.Action;
        dash.dashSpeed = _playerDataSO.dashSpeed;
        dash.controlStats = _controlStats;
        dash.dashDuration= _playerDataSO.dashDuration;
        dash.position = transform.position;
        dash.rb = _rb;
        StateTransitor(DashAction);
        _anim.SetBool("Dash", true);
        _playerDataSO.Damagable = Invulnerability.NoDamagable;
        StartCoroutine(InvulnerabilityTime(_playerDataSO.dashDuration, "Dash"));
    }
    private IEnumerator InvulnerabilityTime(float time, string animation)
    {
        yield return new WaitForSeconds(time);
        _anim.SetBool(animation, false);
        _playerDataSO.Damagable = Invulnerability.Damagable;
        StateTransitor(Walk);
    }
}
