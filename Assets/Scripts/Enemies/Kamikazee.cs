using TMPro;
using UnityEngine;
public enum KamikazeeMovement
{
    Movement,
   // AvoidObstacle,
    Explode,
    Explosion
}
public class Kamikazee : Enemy
{
    private Rigidbody2D _rb;
    private KamikazeeMovement _movement;
    [SerializeField]
    private LayerMask wallMask;
    [SerializeField]
    private float explosionTimer;
    private float _counTillDisapear;
    private Animator _anim;
    [SerializeField]
    private LayerMask _playerMask;
    protected override void Start()
    {
        base.Start();
        _movement = KamikazeeMovement.Movement;
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _anim= GetComponent<Animator>();
    }
    protected override void Update()
    {
        base.Update();
        transform.rotation = FindTarget();
        switch (_movement)
        {
            case KamikazeeMovement.Movement:
                Movement(transform.right.x, transform.right.y);
               IsGoingToExplote();
                break;
            case KamikazeeMovement.Explode:
            case KamikazeeMovement.Explosion:
                Explosion();
                break;
        }
    }
   /* private void ChangeDirectionWhenImpact()
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
    }*/

    /*private void PrioritzateMovement(Vector2 direction, Vector2 playerPosition)
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
    }*/

 /*   private bool ContactWithAnObject(Vector2 direction)
    {
        return Physics2D.Raycast(transform.position, direction, 0.65f);
    }*/
    public override void Movement(float directionX, float directionY)
    {
        if (cloneEnemyData.Damagable == Invulnerability.Damagable&&!cloneEnemyData._stunned)
            _rb.velocity = cloneEnemyData.speed * Time.fixedDeltaTime * new Vector3(directionX, directionY);
    }

   private void IsGoingToExplote()
    {
        if (Physics2D.Raycast(transform.position,_player.transform.position,10,_playerMask)&& !cloneEnemyData._stunned)
        {
            _anim.SetBool("GoingToExplote", true);
            _rb.constraints = RigidbodyConstraints2D.FreezeAll;
            _movement = KamikazeeMovement.Explode;
        }
    }

    private void Explosion()
    {
        StopMomentum();
        if (explosionTimer > _counTillDisapear)
            _counTillDisapear += Time.deltaTime;
        else
        { 
            cloneEnemyData.Damagable = Invulnerability.NoDamagable;
            _movement = KamikazeeMovement.Explosion;
            _anim.SetFloat("ExplosionTimer", _counTillDisapear);
        }
    }
    public void Death()
    {
        State = Life.Death;
    }
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.gameObject.CompareTag("Player")&&_movement==KamikazeeMovement.Explosion)
        {
             Vector3 playerDirection = collision.gameObject.transform.position - transform.position;
             KamikazeeData kamikazeeData = (KamikazeeData)cloneEnemyData;
             collision.gameObject.GetComponent<PlayerController>().TakeDamage(kamikazeeData.explosionDamage);
            collision.gameObject.GetComponent<PlayerController>().GetImpulse(playerDirection.normalized * kamikazeeData.explosionImpulse);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (_movement == KamikazeeMovement.Explosion && collision.gameObject.CompareTag("Player"))
        {
            Vector3 playerDirection = collision.gameObject.transform.position - transform.position;
            KamikazeeData kamikazeeData = (KamikazeeData)cloneEnemyData;
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(kamikazeeData.explosionDamage);
            collision.gameObject.GetComponent<PlayerController>().GetImpulse(playerDirection.normalized * kamikazeeData.explosionImpulse);
        }
    }
}
