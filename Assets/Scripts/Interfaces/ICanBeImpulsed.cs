using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICanBeImpulsed
{
    public abstract void StopMomentum();
    public abstract void GetImpulse(Vector2 impulse);
}
