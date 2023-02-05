using System.Collections;
using UnityEngine;

public abstract class Character : StateController, ICanBeImpulsed
{
    [SerializeField]
    protected AudioClip getDamaged;
    public ScriptableState Stop;
    public abstract void MovementBehaivour(float directionX, float directionY);
    public abstract void TakeDamage(float damage);
   public abstract void OnDeath();

    public virtual void StopMomentum()
    {
        var stop = (ScriptableStopMomentum)Stop.Action;
        stop.rb = gameObject.GetComponent<Rigidbody2D>();
        StateTransitor(Stop);
    }

    public virtual void GetImpulse(Vector2 impulse)
    {
        StopMomentum();
        gameObject.GetComponent<Rigidbody2D>().AddForce(impulse);
    }
}
