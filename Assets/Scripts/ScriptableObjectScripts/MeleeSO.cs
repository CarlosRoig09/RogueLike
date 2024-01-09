using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MeleeSO", menuName = "MeleeSO")]
public class MeleeSO : ScriptableObject 
{
    public List<WeaponAttack> WeaponAttacks;
    public int CurrentAttack = 0;
    public float CadenceTime;
    public bool meleeAttack;
}
