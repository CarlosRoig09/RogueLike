using UnityEngine;

public class Enemy : Character, IDestroyable
{
    [SerializeField]
    protected EnemyData enemyData;
    public Life State;
    private float currentLife;
    protected Vector3 lookDirection;
    private float lookAngle;
    private GameObject _player;
    [SerializeField]
    private DropegableItems _items;
    public GameObject Player
    {
        get => _player;
    }
    protected virtual void Start()
    {
        currentLife = enemyData.maxlife;
        State = Life.Alive;
        _player = GameObject.Find("Player");
    }
    protected virtual void Update()
    {
        FindTarget();
    }
    protected void DealContactDamage(GameObject gameObject)
    {
        gameObject.GetComponent<Player>().TakeDamage(enemyData.ContactDamage);
    }
    protected void FindTarget()
    {
        lookDirection = _player.transform.position - transform.position;
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, lookAngle);
    }
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        //No muy bien equilibrado aún
       /* if (collision.gameObject.CompareTag("Player"))
            DealContactDamage(collision.gameObject); */
    }
    public override void TakeDamage(float damage)
    {
        gameObject.GetComponent<LifeControler>().ModifyLife(damage * -1,ref currentLife, enemyData.maxlife);
    }
    public override void OnDeath()
    {
        State = Life.Death;
    }
    public void GetHitByPlayer(float damage)
    {
        TakeDamage(damage);
    }

    public void Destroyed()
    {
        DropAnObject();
        Destroy(gameObject);
    }

    public bool DropAnObject()
    {
        foreach (var item in _items.Items)
        {
            if (Random.Range(1, 101) <= item.RateAperance)
            {
                Instantiate(item.prefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                return true;
            }
        }
        return false;
    }

    public override void Movement(float directionX, float directionY)
    {
        //Is for the childs if they move
        throw new System.NotImplementedException();
    }
}
