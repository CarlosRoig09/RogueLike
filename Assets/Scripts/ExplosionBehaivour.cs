using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBehaivour : MonoBehaviour
{
    public float ExplosionDamage;
    public float ExplosionImpulse;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDestroyable destroyable = null;
        Character character;
        if ((character = collision.gameObject.GetComponent<Character>()) != null || (destroyable = collision.gameObject.GetComponent<IDestroyable>()) != null)
        {
            if (character != null)
            {
                Vector3 playerDirection = collision.gameObject.transform.position - transform.position;
                collision.gameObject.GetComponent<Character>().GetImpulse(playerDirection.normalized * ExplosionImpulse);
            }
            if (destroyable != null)
                collision.gameObject.GetComponent<IDestroyable>().GetHitByPlayer(ExplosionDamage);
            else
                collision.gameObject.GetComponent<Character>().TakeDamage(ExplosionDamage);
        }
    }
}
