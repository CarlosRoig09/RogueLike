using System.Collections;
using UnityEngine;

public class Enemy : Character, IDestroyable, IGivePuntuation
{
    [SerializeField]
    private EnemyData enemyData;
    protected EnemyData cloneEnemyData;
    public Life State;
    protected Vector3 lookDirection;
    private float lookAngle;
    protected GameObject _player;
    [SerializeField]
    private DropegableItems _items;
    protected GameObject _wall;
    [SerializeField]
    private float _chanceOfDropNothing;
    public GameObject Player
    {
        get => _player;
    }
    protected virtual void Start()
    {
        cloneEnemyData = Instantiate(enemyData);
        cloneEnemyData._stunned = false;
        cloneEnemyData.Damagable = Invulnerability.Damagable;
        _player = GameObject.Find("Player");
    }
    protected virtual void Update()
    {
        FindTarget();
    }

    public void Stunned(float time)
    {
        StopMomentum();
        cloneEnemyData._stunned = true;
        StartCoroutine(StunTime(time));
    }

    public void DeStunned()
    {
        StopCoroutine(StunTime(0));
        cloneEnemyData._stunned = false;
    }
    protected void FindTarget()
    {
        if (!cloneEnemyData._stunned)
        {
            lookDirection = _player.transform.position - transform.position;
            lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, lookAngle);
        }
    }
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        //No muy bien equilibrado aún
       /* if (collision.gameObject.CompareTag("Player"))
            DealContactDamage(collision.gameObject); */
    }
    public override void TakeDamage(float damage)
    {
        gameObject.GetComponent<LifeControler>().ModifyLife(damage * -1, ref cloneEnemyData.life, cloneEnemyData.maxlife);
    }
    public override void OnDeath()
    {
        State = Life.Death;
        GivePuntuation(cloneEnemyData.PuntuationXDeath);
    }
    public void GetHitByPlayer(float damage)
    {
        GivePuntuation(cloneEnemyData.PuntuationXHit);
        TakeDamage(damage);
            cloneEnemyData.Damagable = Invulnerability.NoDamagable;
            StartCoroutine(InvulnerabilityTime(0.5f));
    }

    public void Destroyed()
    {
        DropAnObject();
        Destroy(gameObject);
    }

    public bool DropAnObject()
    {
      var item = RandomMethods.ReturnARandomObject(_items.Items, 50, _items.Items.Length, 0);
        if (item > -1)
        {
            Instantiate(_items.Items[item].prefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            return true;
        }
        return false;
    }

    public override void Movement(float directionX, float directionY)
    {
        //Is for the childs if they move
    }

    private IEnumerator InvulnerabilityTime(float time)
    {
        yield return new WaitForSeconds(time);
        cloneEnemyData.Damagable = Invulnerability.Damagable;
        StopMomentum();
    }

    private IEnumerator StunTime(float time)
    {
        yield return new WaitForSeconds(time);
        cloneEnemyData._stunned = false;
    }

   protected void SetPuntuation(float Puntuation)
    {
       
    }
    public void GivePuntuation(float Puntuation)
    {
        GameManager.Instance.Puntuation += Puntuation;
    }
}
