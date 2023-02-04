using System.Collections;
using UnityEngine;

public abstract class Character : StateController, ICanBeImpulsed
{
    public abstract void Movement(float directionX, float directionY);
    public abstract void TakeDamage(float damage);
   public abstract void OnDeath();

    public void StopMomentum()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0,0);
    }

    public void GetImpulse(Vector2 impulse)
    {
        StopMomentum();
        gameObject.GetComponent<Rigidbody2D>().AddForce(impulse);
    }

    protected IEnumerator CountDownForState(float time)
    {
        yield return new WaitForSeconds(time);
    }
}
