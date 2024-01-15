using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExplosion : ExplosionBehaivour<Character> 
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        IDestroyable destroyable = null;
        if((destroyable = collision.gameObject.GetComponent<IDestroyable>()) != null)
        {
            if (destroyable != null)
                collision.gameObject.GetComponent<IDestroyable>().GetHitByPlayer(ExplosionDamage);
        }
    }
}
