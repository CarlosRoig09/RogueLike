using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
public class Kamikazee : Enemy, ISpawnExplosion
{
    public ScriptableState Movement, Explode;
    private Rigidbody2D _rb;
    [SerializeField]
    private LayerMask wallMask;
    [SerializeField]
    private float explosionTimer;
    private Animator _anim;
    private KamikazeeData kamikazeeData;
    [SerializeField]
    private LayerMask _playerMask;
    private ScriptableState _movement;
    private ScriptableState _explode;
    protected override void Start()
    {
        base.Start();
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _anim= GetComponent<Animator>();
        kamikazeeData = (KamikazeeData)cloneEnemyData;
        _movement = ScriptableStateMethods.CopyAStateMachineState(Movement,new List<ScriptableState>());
        _explode = ScriptableStateMethods.ReturnStateWithId(_movement.ScriptableStateTransitor, Explode.Id);
        stop = ScriptableStateMethods.ReturnStateWithId(_movement.ScriptableStateTransitor, Stop.Id);
        currentState = _movement;
    }
    protected override void Update()
    {
        if (currentState == _movement)
        {
            MovementBehaivour(transform.right.x, transform.right.y);
            IsGoingToExplote();
        }
        base.Update();
        transform.rotation = FindTarget();
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
       var movement = (ScriptableMovement)_movement.Action;
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
            Explosion();
        }
    }

    private void Explosion()
    {
        StopMomentum(0);
        var explode = (ScriptableExplosion)_explode.Action;
        explode.OnExplosion += SpawnExplosion;
        explode.Timer = explosionTimer;
        cloneEnemyData.Damagable = Invulnerability.NoDamagable;
        StateTransitor(_explode);
    }
    public void Death()
    {
        var explode = (ScriptableExplosion)_explode.Action;
        explode.OnExplosion -= SpawnExplosion;
        StopAllCoroutines();
        StopMomentum();
        State = Life.Death;
    }

    public override void InvulnerabilityDeath()
    {
        base.InvulnerabilityDeath();
        var explode = (ScriptableExplosion)_explode.Action;
        explode.OnExplosion -= SpawnExplosion;
        StopAllCoroutines();
    }

    private void OnDestroy()
    {
        var explode = (ScriptableExplosion)_explode.Action;
        explode.OnExplosion -= SpawnExplosion;
    }
    public void SpawnExplosion()
    {
       var explosion = Instantiate(kamikazeeData.explosion,transform.position,Quaternion.identity);
        explosion.GetComponent<ExplosionBehaivour<PlayerController>>().ExplosionDamage = kamikazeeData.explosionDamage;
        explosion.GetComponent<ExplosionBehaivour<PlayerController>>().ExplosionImpulse = kamikazeeData.explosionImpulse;
        StateTransitor(_explode);
        BeforeExplosion();
    }

    public void BeforeExplosion()
    {
        Death();
    }
}
