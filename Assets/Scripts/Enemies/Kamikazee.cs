using TMPro;
using UnityEngine;
public enum KamikazeeMovement
{
    Movement,
   // AvoidObstacle,
    Explode,
    Explosion
}
public class Kamikazee : Enemy, ISpawnExplosion
{
    public ScriptableState Movement, Explode;
    private Rigidbody2D _rb;
    private KamikazeeMovement _movement;
    [SerializeField]
    private LayerMask wallMask;
    [SerializeField]
    private float explosionTimer;
    private Animator _anim;
    private KamikazeeData kamikazeeData;
    [SerializeField]
    private LayerMask _playerMask;
    protected override void Start()
    {
        base.Start();
        _movement = KamikazeeMovement.Movement;
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _anim= GetComponent<Animator>();
        kamikazeeData = (KamikazeeData)cloneEnemyData;
    }
    protected override void Update()
    {
        MovementBehaivour(transform.right.x, transform.right.y);
        base.Update();
        transform.rotation = FindTarget();
        if(currentState==Movement)
        IsGoingToExplote();
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
            _movement = KamikazeeMovement.MovementBehaivour;
    }*/

    /*private void PrioritzateMovement(Vector2 direction, Vector2 playerPosition)
    {
        if (direction.x == 0)
        {
                if (playerPosition.y >= transform.position.y)
                    _movement = KamikazeeMovement.MovementBehaivour;
                else
                {
                    if (Physics2D.Raycast(transform.position, new Vector2(playerPosition.x, 0), playerPosition.x-transform.position.x, wallMask))
                        MovementBehaivour((new Vector2(transform.right.x, 0)).normalized.x*-1, 0);
                    else
                        MovementBehaivour(new Vector2(transform.right.x, 0).normalized.x, 0);
                }
        }
        else
        {
            if (direction.x < 0)
            {
                if (playerPosition.x <= transform.position.x)
                    _movement = KamikazeeMovement.MovementBehaivour;
                else
                {
                    if (Physics2D.Raycast(transform.position, new Vector2(playerPosition.y, 0), Mathf.Infinity, wallMask))
                        MovementBehaivour((new Vector2(transform.right.y, 0)).normalized.y * -1, 0);
                    else
                        MovementBehaivour(new Vector2(transform.right.y, 0).normalized.y, 0);
                }
            }
            else
            {
                if (playerPosition.x >= transform.position.x)
                    _movement = KamikazeeMovement.MovementBehaivour;
            }
        }
    }*/

 /*   private bool ContactWithAnObject(Vector2 direction)
    {
        return Physics2D.Raycast(transform.position, direction, 0.65f);
    }*/
    public override void MovementBehaivour(float directionX, float directionY)
    {
       var movement = (ScriptableMovement)Movement.Action;
        movement.directionX = directionX;
        movement.directionY = directionY;
        movement.speed = cloneEnemyData.speed;
        movement.rb = _rb;
        cloneEnemyData.Damagable = Invulnerability.Damagable;
    }

   private void IsGoingToExplote()
    {
        if ((_player.transform.position-transform.position).magnitude<=3)
        {
            _anim.SetBool("GoingToExplote", true);
            _rb.constraints = RigidbodyConstraints2D.FreezeAll;
            Explosion();
        }
    }

    private void Explosion()
    {
        StopMomentum();
        var explode = (ScriptableExplosion)Explode.Action;
        explode.OnExplosion += SpawnExplosion;
        explode.Timer = explosionTimer;
        cloneEnemyData.Damagable = Invulnerability.NoDamagable;
        StateTransitor(Explode);
    }
    public void Death()
    {
        StopMomentum();
        State = Life.Death;
    }
    public void SpawnExplosion()
    {
        var kamikazee = (KamikazeeData)cloneEnemyData;
       var explosion = Instantiate(kamikazee.explosion,transform.position,Quaternion.identity);
        explosion.GetComponent<ExplosionBehaivour>().ExplosionDamage = kamikazee.explosionDamage;
        explosion.GetComponent<ExplosionBehaivour>().ExplosionImpulse = kamikazee.explosionImpulse;
        StateTransitor(Explode);
        BeforeExplosion();
    }

    public void BeforeExplosion()
    {
        Death();
    }
}
