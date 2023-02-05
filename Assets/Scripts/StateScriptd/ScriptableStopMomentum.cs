using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "ScriptableStopMomentum", menuName = "ScriptableStopMomentum")]
public class ScriptableStopMomentum : ScriptableAction
{
    public Rigidbody2D rb;
    public override void OnSetState()
    {
        rb.velocity= Vector3.zero;
    }

    public override void OnFinishedState()
    {
    }

    public override void OnUpdate()
    {
    }
}
