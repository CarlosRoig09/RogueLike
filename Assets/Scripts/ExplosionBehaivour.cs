using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ExplosionBehaivour<T> : MonoBehaviour where T : Character
{
    public float ExplosionDamage;
    public float ExplosionImpulse;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.Play("Explosion");
        Destroy(gameObject,GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        IDestroyable destroyable = null;
        T character;
        if ((character = collision.gameObject.GetComponent<T>()) != null || (destroyable = collision.gameObject.GetComponent<IDestroyable>()) != null)
        {
            if (character != null)
            {
                collision.gameObject.GetComponent<T>().TakeDamage(ExplosionDamage);
                Vector3 playerDirection = collision.gameObject.transform.position - transform.position;
                collision.gameObject.GetComponent<T>().GetImpulse(playerDirection.normalized * ExplosionImpulse);
            }
        }
    }
}
