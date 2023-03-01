using UnityEngine;

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
