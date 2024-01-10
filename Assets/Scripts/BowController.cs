using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowController : MonoBehaviour, IWeaponControler
{
    private Collider2D _collider2D;
    public WeaponData WeaponSO { get => _weaponSO; set => _weaponSO = value; }
    private WeaponData _weaponSO;
    private GameObject _parent;
    private GameObject _grandParent;
    private ShootControler _controler;
    private float _weaponDamage;
    public float WeaponDamage { get => _weaponDamage; set => _weaponDamage = _weaponSO.meleeData.WeaponAttacks[_weaponSO.meleeData.CurrentAttack].Damage * value; }
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
        _controler = gameObject.GetComponent<ShootControler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_weaponSO.WA != WeaponState.Item)
        {
            if (_weaponSO.WA == WeaponState.Normal)
                _parent.transform.rotation = _parent.transform.rotation.z != 0 ? new Quaternion(0,0,0,0) : _parent.transform.rotation;
            if(_weaponSO.WA == WeaponState.MeleeAttack)
                StartCoroutine(_weaponSO.WeaponSpin(1, _parent, WeaponState.Stop));
            if (_weaponSO.WA == WeaponState.Stop)
                EndDistanceAttack();
            if (_weaponSO.WA == WeaponState.DistanceAttack)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    EndMeleeAttack();
                    _weaponSO.meleeData.meleeAttack = true;
                }
            }
            if (_weaponSO.WA == WeaponState.ComeBack)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    Debug.Log("Shoot");
                    var shootControler = _controler;
                    var shootForProyectile = Instantiate(WeaponSO.shootData);
                    shootForProyectile.ProyectileDamage = ProyectileDamage;
                    shootForProyectile.CadenceTime = ProyectileSpeed;
                    shootControler.NewWeapon(shootForProyectile);
                    shootControler.ProyectileSpawn();
                    EndMeleeAttack();
                }
            }
        }
    }

    public void SecondButtonAttack()
    {
      if (WeaponSO.WA == WeaponState.Normal && WeaponSO.shootData.rangeAttack)
        {
            Debug.Log("Distance attack");
            CollisionEnable();
            _weaponSO.WA = WeaponState.MeleeAttack;
        }
    }

    private void EndDistanceAttack()
    {
        CollisionDisable();
        _weaponSO.meleeData.meleeAttack = false;
        _weaponSO.WA = WeaponState.Normal;
        StartCoroutine(_weaponSO.ResetMeleeCount());
    }
    public void FirstButtonAttack()
    {
        _weaponSO.AttackByAnimator(gameObject,WeaponState.DistanceAttack);
    }

    private void RelaseBow()
    {
        Debug.Log("Relase");
        _weaponSO.WA = WeaponState.ComeBack;
    }

    public void EndMeleeAttack()
    {
        _weaponSO.AttackAnimationIsFinished(gameObject, ref _weaponSO.shootData.rangeAttack);
        StartCoroutine(_weaponSO.ResetProyectileCount());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (enabled)
        {
            if (_weaponSO.WA != WeaponState.Item)
            {
                if (collision.gameObject.GetComponent<IDestroyable>() != null)
                    collision.gameObject.GetComponent<IDestroyable>().GetHitByPlayer(_weaponSO.WA == WeaponState.MeleeAttack ? WeaponDamage : ProyectileDamage);

                if (collision.CompareTag("Enemy") || collision.CompareTag("Proyectile"))
                    PushOtherGO(collision, _weaponSO.WA == WeaponState.MeleeAttack ? _weaponSO.meleeData.WeaponAttacks[_weaponSO.meleeData.CurrentAttack].ImpulseForce : _weaponSO.shootData.ImpulseForce);
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
