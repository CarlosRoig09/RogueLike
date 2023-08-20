using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableState", menuName = "ScriptableStatet")]
public class ScriptableState : ScriptableObject
{
    public ScriptableAction Action;
    public List<ScriptableState> ScriptableStateTransitor;
    public string Id;
}
