using UnityEngine;
public enum KamikazeeMovement
{
    Movement,
    AvoidObstacle,
    Explode
}
public class Kamikazee : Enemy
{
    private Rigidbody2D _rb;
    private KamikazeeMovement _movement;
    private GameObject _wall;
    [SerializeField]
    private LayerMask wallMask;
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
            case KamikazeeMovement.AvoidObstacle:
                ChangeDirectionWhenImpact();
                break;
            case KamikazeeMovement.Explode:
                Explosion();
                break;
        }
    }
    private void ChangeDirectionWhenImpact()
    {
        if (ContactWithAnObject(Vector2.down))
            PrioritzateMovement(Vector2.down, _player.transform.position);
        else if (ContactWithAnObject(Vector2.up))
            PrioritzateMovement(Vector2.up, _player.transform.position);
        else if ((ContactWithAnObject(Vector2.right)))
            PrioritzateMovement(Vector2.right, _player.transform.position);
        else if ((ContactWithAnObject(Vector2.left)))
            PrioritzateMovement(Vector2.left, _player.transform.position);
        else
            _movement = KamikazeeMovement.Movement;
    }

    private void PrioritzateMovement(Vector2 direction, Vector2 playerPosition)
    {
        if (direction.x == 0)
        {
                if (playerPosition.y >= transform.position.y)
                    _movement = KamikazeeMovement.Movement;
                else
                {
                    if (Physics2D.Raycast(transform.position, new Vector2(playerPosition.x, 0), playerPosition.x-transform.position.x, wallMask))
                        Movement((new Vector2(transform.right.x, 0)).normalized.x*-1, 0);
                    else
                        Movement(new Vector2(transform.right.x, 0).normalized.x, 0);
                }
        }
        else
        {
            if (direction.x < 0)
            {
                if (playerPosition.x <= transform.position.x)
                    _movement = KamikazeeMovement.Movement;
                else
                {
                    if (Physics2D.Raycast(transform.position, new Vector2(playerPosition.y, 0), Mathf.Infinity, wallMask))
                        Movement((new Vector2(transform.right.y, 0)).normalized.y * -1, 0);
                    else
                        Movement(new Vector2(transform.right.y, 0).normalized.y, 0);
                }
            }
            else
            {
                if (playerPosition.x >= transform.position.x)
                    _movement = KamikazeeMovement.Movement;
            }
        }
    }

    private bool ContactWithAnObject(Vector2 direction)
    {
        return Physics2D.Raycast(transform.position, direction, 0.65f);
    }
    public override void Movement(float directionX, float directionY)
    {
        if (enemyData.Damagable == Invulnerability.Damagable)
            _rb.velocity = enemyData.speed * Time.fixedDeltaTime * new Vector3(directionX, directionY);
    }

    private void IsGoingToExplote()
    {
        if (lookDirection.magnitude<=2)
            _movement = KamikazeeMovement.Explode;
    }

    private void Explosion()
    {
        StopMomentum();
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
        if (collision.gameObject.layer == 7)
        {
            _movement = KamikazeeMovement.AvoidObstacle;
            _wall = collision.gameObject;
        }
    }
}
