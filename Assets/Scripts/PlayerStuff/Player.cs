using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Invulnerability
{
    Damagable,
    NoDamagable,
}
public class Player : Character
{
    
    public PlayerData _playerDataSO;
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
    void Update()
    {
        ChangeSpriteByRotation();
    }

    private void ChangeSpriteByRotation()
    {
        Vector3 vector = gameObject.GetComponentInParent<Player>().VectorMousePlayerAngle();
        float angle = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
        float xPosition = Mathf.Cos(Mathf.Deg2Rad * angle);
        float yPosition = Mathf.Sin(Mathf.Deg2Rad * angle);
        _anim.SetFloat("Movement", Mathf.Abs(_rb.velocity.x) + Mathf.Abs(_rb.velocity.y));
        _anim.SetFloat("MPositionX", xPosition);
        _anim.SetFloat("MPositionY", yPosition);
    }
    public override void Movement(float directionX, float directionY)
    {
        if(_playerDataSO.Damagable==Invulnerability.Damagable)
        _rb.velocity = new Vector3(directionX * _playerDataSO.speed * Time.fixedDeltaTime, directionY * _playerDataSO.speed * Time.fixedDeltaTime, transform.position.z);
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
        _weapon.FirstButtonAttack();
    }
    public void Shoot()
    {
        _weapon.SecondButtonAttack();
    }
    public override void OnDeath()
    {
        _playerDataSO.State = Life.Death;
    }
    public void Dash()
    {
        _anim.SetBool("Dash", true);
        var vectorPM = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
     _rb.AddForce(vectorPM.normalized * _playerDataSO.dashSpeed,ForceMode2D.Impulse);
        _playerDataSO.Damagable = Invulnerability.NoDamagable;
        StartCoroutine(InvulnerabilityTime(_playerDataSO.dashDuration, "Dash"));
        _controlStats.ModificadorDeStat(Type.WeaponsDamage,1.5f,_playerDataSO.dashDuration);
    }
    private IEnumerator InvulnerabilityTime(float time, string animation)
    {
        yield return new WaitForSeconds(time);
        _anim.SetBool(animation, false);
        _playerDataSO.Damagable = Invulnerability.Damagable;
        StopMomentum();
    }
}
