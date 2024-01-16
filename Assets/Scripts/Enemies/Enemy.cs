using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : Character, IDestroyable, IGivePuntuation
{
    [SerializeField]
    private EnemyData enemyData;
    protected EnemyData cloneEnemyData;
    private ScriptableState previousState;
    public Life State;
    protected Vector3 lookDirection;
    private float lookAngle;
    protected GameObject _player;
    [SerializeField]
    private DropegableItems _items;
    protected GameObject _wall;
    private Animator _animator;
    [SerializeField]
    private float _chanceOfDropNothing;
    private int _lastAnimation;
    protected ScriptableState CloneStop;
    private bool _playerDetected;
    private bool _firstTime;
    private bool _firstFindedTarget;
    private ScriptableState _beforeStop;
    public GameObject Player
    {
        get => _player;
    }
    protected virtual void Start()
    {
        _playerDetected = false;
        _firstTime = true;
        _firstFindedTarget = true;
        CloneStop = Instantiate(Stop);
        _animator = GetComponent<Animator>();
        cloneEnemyData = Instantiate(enemyData);
        cloneEnemyData._stunned = false;
        cloneEnemyData.Damagable = Invulnerability.Damagable;
        _player = GameObject.Find("Player");
    }
    protected override void Update()
    {
        base.Update();
        FindTarget();
    }

    public void Stunned()
    {
        _animator.enabled = false;
    }

    public void DeStunned()
    {
        _animator.enabled = true;
        StateTransitor(previousState);
    }
    protected Quaternion FindTarget()
    {
        if (!cloneEnemyData._stunned&&_playerDetected)
        {
            if(_firstFindedTarget)
            {
                StateTransitor(_beforeStop);
                _firstFindedTarget = false;
            }
            lookDirection = _player.transform.position - transform.position;
            lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
            return Quaternion.Euler(0, 0, lookAngle);
        }
        if (_firstTime)
        {
            _beforeStop = currentState;
            StopMomentum();
            _firstTime = false;
        }
        return Quaternion.Euler(0, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _player = collision.gameObject;
            _playerDetected = true;
        }
    }
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        //No muy bien equilibrado aún
       /* if (collision.gameObject.CompareTag("PlayerController"))
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
    public void GetHitByPlayer(float damage,float time)
    {
        AudioManager.instance.Play("HitEnemy");
        GivePuntuation(cloneEnemyData.PuntuationXHit);
        TakeDamage(damage);
            cloneEnemyData.Damagable = Invulnerability.NoDamagable;
        _animator.SetBool("Damage", true);
            StartCoroutine(InvulnerabilityTime(0.5f,time));
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

    public override void MovementBehaivour(float directionX, float directionY)
    {
        //Is for the childs if they move
    }

    private IEnumerator InvulnerabilityTime(float time,float stunTime)
    {
        StopMomentum(stunTime);
        yield return new WaitForSeconds(time);
        cloneEnemyData.Damagable = Invulnerability.Damagable;
        _animator.SetBool("Damage", false);
    }

    public void StopMomentum(float time)
    {
        var action = (ScriptableStopMomentum)stop.Action;
        action.rb = gameObject.GetComponent<Rigidbody2D>();
        if(currentState!=stop)
        previousState = currentState;
        StateTransitor(stop);
        StartCoroutine(StunTime(time));
    }

    public override void GetImpulse(Vector2 impulse)
    {

        gameObject.GetComponent<Rigidbody2D>().AddForce(impulse);
    }

    private IEnumerator StunTime(float time)
    {
        yield return new WaitForSeconds(time);
        StateTransitor(previousState);
    }
    public void GivePuntuation(int Puntuation)
    {
        GameManager.Instance.Puntuation += Puntuation;
    }

    public override void InvulnerabilityDeath()
    {
        GetComponent<Collider2D>().enabled=false;
        AudioManager.instance.Play("EnemyDead");
        StopMomentum();
        cloneEnemyData.Damagable = Invulnerability.NoDamagable;
    }
}
