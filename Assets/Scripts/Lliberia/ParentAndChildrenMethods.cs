using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ParentAndChildrenMethods 
{
    public static void UnParentAllChildren(GameObject parent)
    {
        parent.transform.DetachChildren();
    }

    public static void UnParentAnSpecificChildren(GameObject parent, GameObject chilldren)
    {
        if (parent == chilldren.transform.parent.gameObject)
            chilldren.transform.parent = null;
        else throw new System.Exception("ERROR: The child gameObject " + chilldren.name + " doesn't pertaint to this parent " + parent.name);
    }

    public static void ParentAChildren(GameObject parent, GameObject chilldren)
    {
        chilldren.transform.parent = parent.transform;
    }
}
