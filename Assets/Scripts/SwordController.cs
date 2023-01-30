using System.Collections;
using UnityEngine;
public enum WeaponState
{
    Item,
    Normal,
    MeleeAttack,
    EndMeleeAttack,
    DistanceAttack,
    Stop,
    ComeBack
}
public class SwordController : MonoBehaviour,IWeaponControler
{
    private Collider2D _collider2D;
    public WeaponData WeaponSO { get => _weaponSO; set => _weaponSO = value; }
    private WeaponData _weaponSO;
    private GameObject _parent;
    private GameObject _grandParent;
    private float _weaponDamage;
    public float WeaponDamage { get => _weaponDamage; set => _weaponDamage = _weaponSO.meleeData.Damage * value; }
    private float _weaponSpeed;
    public float WeaponSpeed { get => _weaponSpeed; set => _weaponSpeed = _weaponSO.meleeData.CadenceTime * value; }
    private float _proyectileSpeed;
    public float ProyectileSpeed { get => _proyectileSpeed; set => _proyectileSpeed = _weaponSO.shootData.ProyectileSpeed * value; }
    private float _proyectileDamage;
    public float ProyectileDamage { get => _proyectileDamage; set => _proyectileDamage = _weaponSO.shootData.ProyectileDamage * value; }
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
            _weaponSO.MaxDistance(_parent, WeaponState.ComeBack);
            _weaponSO.ComeBack(_grandParent, _parent, 1.5f,ProyectileSpeed);
            if (_weaponSO.WA == WeaponState.ComeBack)
                CollisionDisable();
            else
                if (!_weaponSO.shootData.rangeAttack)
                StartCoroutine(_weaponSO.ResetProyectileCount());
        }
    }

    public void SecondButtonAttack()
    {
        if (WeaponSO.WA == WeaponState.Normal && WeaponSO.shootData.rangeAttack)
        {
            CollisionEnable();
            _weaponSO.PrepareToThrowWeapon(_grandParent, _parent);
            _weaponSO.ThrowWeapon(_parent,ProyectileSpeed);
        }
    }
    public void FirstButtonAttack()
    {
        _weaponSO.AttackByAnimator(gameObject, WeaponState.MeleeAttack);
    }

    public void EndMeleeAttack()
    {
        _weaponSO.AttackAnimationIsFinished(gameObject, ref _weaponSO.meleeData.meleeAttack);
        StartCoroutine(_weaponSO.ResetMeleeCount());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (enabled)
        {
            if (_weaponSO.WA != WeaponState.Item)
            {
                if (collision.gameObject.GetComponent<IDestroyable>() != null)
                    collision.gameObject.GetComponent<IDestroyable>().GetHitByPlayer(_weaponSO.WA == WeaponState.MeleeAttack ? WeaponDamage : ProyectileDamage);

                if (collision.CompareTag("Enemy")||collision.CompareTag("Proyectile"))
                    PushOtherGO(collision, _weaponSO.WA == WeaponState.MeleeAttack ? _weaponSO.meleeData.ImpulseForce : _weaponSO.shootData.ImpulseForce);
            }
        }
    }
    public void PushOtherGO(Collider2D collision, float impulse)
    {
        Vector3 enemyDirection = collision.gameObject.transform.position - transform.position;
        var canBeImpulse = collision.gameObject.GetComponent<ICanBeImpulsed>();
        if (canBeImpulse != null)
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
