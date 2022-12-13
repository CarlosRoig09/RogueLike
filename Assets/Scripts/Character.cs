using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public abstract void Movement(float directionX, float directionY);
    public abstract void TakeDamage(float damage);
   public abstract void OnDeath();
}
