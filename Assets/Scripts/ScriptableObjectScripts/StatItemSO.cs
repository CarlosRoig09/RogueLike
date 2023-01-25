using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StateItemSO", menuName = "StateItemSO")]
public class StatItemSO : ItemData
{
    public float modUp;
    public bool temporal;
    public Type Stat;
    public float timeDuration;
}
