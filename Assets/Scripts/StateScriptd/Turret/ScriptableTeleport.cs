using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableTeleport", menuName = "ScriptableTeleport")]
public class ScriptableTeleport : ScriptableAction
{
    public Vector3 newPosition;
    public Transform characterTransform;
    public override void OnFinishedState()
    {
    }

    public override void OnSetState()
    {
        characterTransform.position = newPosition;
    }

    public override void OnUpdate()
    {
    }
}
