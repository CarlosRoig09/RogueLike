using UnityEngine;

public class LanceController : MonoBehaviour, IWeaponControler
{
    private Collider2D _collider2D;
    private WeaponData _weaponSO;
    private GameObject _parent;
    private GameObject _grandParent;
    private Enemy _enemyStunned;
    public WeaponData WeaponSO { get => _weaponSO; set => _weaponSO = value; }

    // Start is called before the first frame update
    void Start()
    {
        _parent = transform.parent.gameObject;
        _grandParent = _parent.transform.parent.gameObject;
        _collider2D = gameObject.GetComponent<Collider2D>();
        CollisionDisable();
        _weaponSO.CallInStart();
    }

    // Update is called once per frame
    void Update()
    {
        if (_weaponSO.WA != WeaponState.Item)
        {
            _weaponSO.MaxDistance(_parent, WeaponState.Stop);
            _weaponSO.ComeBack(_grandParent, _parent, 1.5f);
            if (_weaponSO.WA == WeaponState.ComeBack)
            {
                CollisionEnable();
                if(_enemyStunned!= null)
                {
                   _enemyStunned.DeStunned();
                   _enemyStunned.GetHitByPlayer(_weaponSO.shootData.ProyectileDamage);
                    _enemyStunned= null;
                }
            }
            else
                if (!_weaponSO.shootData.rangeAttack)
                StartCoroutine(_weaponSO.ResetProyectileCount());
            if (_weaponSO.WA == WeaponState.Normal || _weaponSO.WA == WeaponState.Stop)
                CollisionDisable();
        }
    }

    public void SecondButtonAttack()
    {
        CollisionEnable();
        if (WeaponSO.WA == WeaponState.Normal && WeaponSO.shootData.rangeAttack)
        {
            _weaponSO.PrepareToThrowWeapon(_grandParent, _parent);
            _weaponSO.ThrowWeapon(_parent);
        }
        else
            if (_weaponSO.WA == WeaponState.Stop) 
            _weaponSO.WA = WeaponState.ComeBack;
    }
    public void FirstButtonAttack()
    {
        _weaponSO.AttackByAnimator(gameObject,WeaponState.MeleeAttack);
    }

    public void EndMeleeAttack()
    {
        _weaponSO.AttackAnimationIsFinished(gameObject,ref _weaponSO.meleeData.meleeAttack);
        StartCoroutine(_weaponSO.ResetMeleeCount());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (enabled)
        {
            if (collision.gameObject.GetComponent<IDestroyable>() != null)
            {
                collision.gameObject.GetComponent<IDestroyable>().GetHitByPlayer(_weaponSO.WA == WeaponState.MeleeAttack ? _weaponSO.meleeData.Damage : _weaponSO.shootData.ProyectileDamage);
                if(_weaponSO.WA == WeaponState.DistanceAttack&&collision.gameObject.CompareTag("Enemy"))
                {
                    _parent.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0);
                    _weaponSO.WA = WeaponState.Stop;
                  _enemyStunned = collision.gameObject.GetComponent<Enemy>();
                    _enemyStunned.Stunned(5);
                }
            }
            

          //  if (collision.CompareTag("Enemy") || collision.CompareTag("Proyectile"))
                //PushOtherGO(collision, _weaponSO.WA == WeaponState.MeleeAttack ? _weaponSO.meleeData.ImpulseForce : _weaponSO.shootData.ImpulseForce);
        }
    }
    public void PushOtherGO(Collider2D collision, float impulse)
    {
        Vector3 enemyDirection = collision.gameObject.transform.position - transform.position;
        if (collision.gameObject.TryGetComponent<ICanBeImpulsed>(out var canBeImpulse))
            canBeImpulse.GetImpulse(enemyDirection.normalized * impulse);
    }
    public void CollisionEnable()
    {
        _collider2D.enabled = true;
    }

    public void CollisionDisable()
    {
        _collider2D.enabled = false;
    }
}
