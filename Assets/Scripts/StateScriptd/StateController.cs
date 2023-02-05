using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    public ScriptableState currentState;
    // Update is called once per frame
   protected virtual void Update()
    {
       currentState.Action.OnUpdate();
    }

    public void StateTransitor(ScriptableState state)
    {
        if (currentState.ScriptableStateTransitor.Contains(state))
        {
            currentState.Action.OnFinishedState();
            currentState = state;
            currentState.Action.OnSetState();
        }
    }
}
