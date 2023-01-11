using UnityEngine;

public class ProyectileBehaivour : MonoBehaviour, ICanBeImpulsed
{
    private ShootSO _shootSO;
    Vector2 lookDirection;
    float lookAngle;
    private Rigidbody2D _rb2D;
    private Vector3 _initialPosition;
    private ProyectileUser _proyectileUser;
    private float _damage;

    public void WeaponType(ShootSO shootSO)
    {
        _shootSO = shootSO;
    }
    public void Tragectory(Vector3 target)
    {
        lookDirection = target - transform.position;
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, lookAngle);
    }
    private void Start()
    {
        _rb2D = gameObject.GetComponent<Rigidbody2D>();
        _proyectileUser = _shootSO.proyectileUser;
        _damage = _shootSO.ProyectileDamage;
        ProyectileMovement();
    }
    void ProyectileMovement()
    {
            _rb2D.velocity = _shootSO.ProyectileSpeed * Time.fixedDeltaTime * transform.right;
    }

    private void DestroyProyectile()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (_proyectileUser)
        {
            case ProyectileUser.Player:
                if (collision.gameObject.GetComponent<IDestroyable>() != null)
                {
                    collision.gameObject.GetComponent<IDestroyable>().GetHitByPlayer(_damage);
                    if (collision.CompareTag("Enemy"))
                        PushOtherGO(collision, _shootSO.ImpulseForce);
                }
                if (!collision.CompareTag("Player")&&!collision.CompareTag("Proyectile")&&!collision.CompareTag("Item")&&!collision.CompareTag("Weapon"))
                    DestroyProyectile();
                break;
            case ProyectileUser.Enemy:
                if (collision.CompareTag("Player"))
                    collision.gameObject.GetComponent<Player>().TakeDamage(_damage);
                if (collision.CompareTag("Weapon"))
                    _proyectileUser = ProyectileUser.Player;
                if (!collision.CompareTag("Enemy") && !collision.CompareTag("Item") && !collision.CompareTag("Weapon"))
                    DestroyProyectile();
                break;
        }
    }

    public void PushOtherGO(Collider2D collision, float impulse)
    {
        Vector3 enemyDirection = collision.gameObject.transform.position - transform.position;
        var canBeImpulse = collision.gameObject.GetComponent<ICanBeImpulsed>();
        if (canBeImpulse != null)
            canBeImpulse.GetImpulse(enemyDirection.normalized * impulse);
    }

    public void StopMomentum()
    {
        _rb2D.velocity = new Vector3(0, 0);
    }

    public void GetImpulse(Vector2 impulse)
    {
        StopMomentum();
        _damage *= 1.5f;
        _rb2D.AddForce(impulse*2f);
    }
}
