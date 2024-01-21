using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class ScriptableStateMethods 
{
    public static ScriptableState CopyAStateMachineState(ScriptableState state, List<ScriptableState> scriptableStates)
    {
        var newState = Object.Instantiate(state);
        newState.Action= Object.Instantiate(state.Action);
        scriptableStates.Add(newState);
        for(var countTransState = 0; countTransState < newState.ScriptableStateTransitor.Count;countTransState++)
        {
            if (!SameIdInList(scriptableStates, newState.ScriptableStateTransitor[countTransState], out var stateValue))
            {
                newState.ScriptableStateTransitor[countTransState] = CopyAStateMachineState(newState.ScriptableStateTransitor[countTransState], scriptableStates);
            }
            else
            {
                newState.ScriptableStateTransitor[countTransState] = stateValue;
            }
        }
        return newState;
    }

    public static bool SameIdInList(List<ScriptableState> states, ScriptableState stateToSearch, out ScriptableState sameState)
    {
        sameState = null;
        foreach(var state in states)
        {
            if (stateToSearch.Id == state.Id)
            {
                sameState = state;
                return true;
            }
        }
        return false;
    }

    public static ScriptableState ReturnStateWithId(List<ScriptableState> scriptableStates, string id)
    {
        foreach (var state in scriptableStates)
        {
            if (id == state.Id)
            {
                return state;
            }
        }
        return null;
    }
}
