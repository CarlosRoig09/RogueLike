using System.Collections;
using UnityEngine;

public abstract class Character : StateController, ICanBeImpulsed
{
    [SerializeField]
    protected AudioClip getDamaged;
    public ScriptableState Stop;
    [SerializeField]
    protected ScriptableState stop;
    public abstract void MovementBehaivour(float directionX, float directionY);
    public abstract void TakeDamage(float damage);
   public abstract void OnDeath();

    public abstract void InvulnerabilityDeath();

    public virtual void StopMomentum()
    {
        var state = (ScriptableStopMomentum)stop.Action;
        state.rb = gameObject.GetComponent<Rigidbody2D>();
        StateTransitor(stop);
    }

    public virtual void GetImpulse(Vector2 impulse)
    {
        StopMomentum();
        gameObject.GetComponent<Rigidbody2D>().AddForce(impulse);
    }
}
