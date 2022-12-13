using UnityEngine;

public class ProyectileBehaivour : MonoBehaviour
{
    private ShootSO _shootSO;
    Vector2 lookDirection;
    float lookAngle;
    private Vector3 _initialPosition;

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
    // Update is called once per frame
    void Update()
    {
        ProyectileMovement();
    }
    void ProyectileMovement()
    {
            gameObject.GetComponent<Rigidbody2D>().velocity = _shootSO.ProyectileSpeed * Time.fixedDeltaTime * transform.right;
        /*if (_shootSO.maxDistance != 0 && _shootSO.currentDistance >= _shootSO.maxDistance)
            DestroyProyectile();
        else
            _shootSO.currentDistance += new Vector3(transform.position.x - _initialPosition.x, transform.position.y - _initialPosition.y).magnitude;*/
    }

    private void DestroyProyectile()
    {
        _shootSO.currentDistance = 0;
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (_shootSO.proyectileUser)
        {
            case ProyectileUser.Player:
                if (collision.gameObject.GetComponent<IDestroyable>() != null)
                    collision.gameObject.GetComponent<IDestroyable>().GetHitByPlayer(_shootSO.ProyectileDamage);
                if (!collision.CompareTag("Player")&&!collision.CompareTag("Proyectile")&&!collision.CompareTag("Item"))
                    DestroyProyectile();
                break;
            case ProyectileUser.Enemy:
                if (collision.CompareTag("Player"))
                    collision.gameObject.GetComponent<Player>().TakeDamage(_shootSO.ProyectileDamage);
                if (!collision.CompareTag("Enemy") && !collision.CompareTag("Proyectile") && !collision.CompareTag("Item"))
                    DestroyProyectile();
                break;
        }
    }
}
