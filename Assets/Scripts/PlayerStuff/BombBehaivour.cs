using System.Collections;
using UnityEngine;

public class BombBehaivour : MonoBehaviour, ISpawnExplosion
{
    [SerializeField]
    private GameObject explosion;
    private Rigidbody2D _rb;
    [SerializeField]
    private float ExplosionTimer;
    [SerializeField]
    private float ExplosionDamage;
    [SerializeField]
    private float ExplosionImpulse;
    public void BeforeExplosion()
    {
        StartCoroutine(WaitTillExplosion(ExplosionTimer));
    }

    public void SpawnExplosion()
    {
       var exp = Instantiate(explosion,transform.position,Quaternion.identity);
        exp.GetComponent<ExplosionBehaivour<Character>>().ExplosionDamage = ExplosionDamage;
        exp.GetComponent <ExplosionBehaivour<Character>>().ExplosionImpulse = ExplosionImpulse;
        Destroy(gameObject);
    }

    public void ThrewBomb(float impulse, Vector3 direction)
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.AddForce(direction* impulse);
        BeforeExplosion();
    }

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    private IEnumerator WaitTillExplosion(float time)
    {
        yield return new WaitForSeconds(time);
        SpawnExplosion();
    }
}
