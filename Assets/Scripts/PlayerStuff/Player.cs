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
    private ShootControler _weapon;
    private Rigidbody2D _rb;
    private Invulnerability _invulnerability;
    private Vector3 _vectorPM;
    // Start is called before the first frame update
    void Start()
    {
        _invulnerability = Invulnerability.Damagable;
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _weapon = GameObject.Find("Weapon").GetComponent<ShootControler>();
        _playerDataSO.life = _playerDataSO.maxlife;
        _playerDataSO.State = Life.Alive;
    }
    // Update is called once per frame
    void Update()
    {
        RotationZByMouse();
    }
    public override void Movement(float directionX, float directionY)
    {
        if(_invulnerability==Invulnerability.Damagable)
        transform.position += new Vector3(directionX * _playerDataSO.speed * Time.deltaTime, directionY * _playerDataSO.speed * Time.deltaTime, transform.position.z);
    }
    void RotationZByMouse()
    {
        _vectorPM = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, Mathf.Atan2(_vectorPM.y, _vectorPM.x) * Mathf.Rad2Deg);
    }

    public override void TakeDamage(float damage)
    {
        if(_invulnerability == Invulnerability.Damagable)
        gameObject.GetComponent<LifeControler>().ModifyLife(damage * -1,ref _playerDataSO.life, _playerDataSO.maxlife);
    }
    public void SumLife(float extraLife)
    {
        gameObject.GetComponent<LifeControler>().ModifyLife(extraLife, ref _playerDataSO.life, _playerDataSO.maxlife);
    }
    public void Shoot()
    {
        _weapon.ProyectileSpawn();
    }
    public override void OnDeath()
    {
        _playerDataSO.State = Life.Death;
    }
    public void Dash()
    {
     _rb.AddForce(transform.right * _playerDataSO.dashSpeed,ForceMode2D.Impulse);
        _invulnerability = Invulnerability.NoDamagable;
        StartCoroutine(StopMomentum(_playerDataSO.dashDuration));
    }
    private IEnumerator StopMomentum(float time)
    {
        yield return new WaitForSeconds(time);
        _invulnerability = Invulnerability.Damagable;
        _rb.velocity = new Vector3(0, 0);
    }
}
