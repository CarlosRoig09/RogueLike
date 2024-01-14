using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExplosion : ExplosionBehaivour<PlayerController> 
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        collision.gameObject.GetComponent<PlayerController>().TakeDamage(ExplosionDamage);
    }
}
