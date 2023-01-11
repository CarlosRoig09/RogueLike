using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MeleeSO", menuName = "MeleeSO")]
public class MeleeSO : ScriptableObject 
{
    public float Damage;
    public float CadenceTime;
    public float ImpulseForce;
    public bool meleeAttack;
}
