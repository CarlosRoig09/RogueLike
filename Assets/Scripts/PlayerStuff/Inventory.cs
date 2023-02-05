using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInventory", menuName = "Inventory")]
public class Inventory : ScriptableObject
{
    public List<WeaponData> Weapons;
    public float LimitWeapons;
    public float Coins;
    public float Bombs;
}
