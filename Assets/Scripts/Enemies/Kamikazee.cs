using UnityEngine;
public enum KamikazeeMovement
{
    Movement,
    Explode
}
public class Kamikazee : Enemy
{
    private Rigidbody2D _rb;
    private KamikazeeMovement _movement;
    [SerializeField]
    private float explosionTimer;
    private float _counTillDisapear;
    protected override void Start()
    {
        base.Start();
        _movement = KamikazeeMovement.Movement;
        _rb = gameObject.GetComponent<Rigidbody2D>();
    }
    protected override void Update()
    {
        base.Update();
        switch (_movement)
        {
            case KamikazeeMovement.Movement:
                Movement(transform.right.x, transform.right.y);
                IsGoingToExplote();
                break;
            case KamikazeeMovement.Explode:
                _rb.velocity = new Vector3(0,0);
                Explosion();
                break;
        }
    }
    public override void Movement(float directionX, float directionY)
    {
        _rb.velocity = enemyData.speed * Time.fixedDeltaTime * new Vector3(directionX, directionY);
    }

    private void IsGoingToExplote()
    {
        if (lookDirection.magnitude<=2)
            _movement = KamikazeeMovement.Explode;
    }

    private void Explosion()
    {
        _rb.velocity = new Vector3(0,0);
        if (explosionTimer > _counTillDisapear)
            transform.localScale = new Vector3(transform.localScale.x + 2f * Time.deltaTime, transform.localScale.y + 2f * Time.deltaTime);
        else State = Life.Death;
        _counTillDisapear += Time.deltaTime;
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (_movement == KamikazeeMovement.Explode && collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(enemyData.ContactDamage);
        }
    }
}
