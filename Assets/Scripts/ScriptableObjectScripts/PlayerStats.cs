using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "PlayerStats")]
public class PlayerStats : ScriptableObject
{
    public float Damage;
    public float Speed;
    public float AttackSpeed;
    public float ProyectileDamage;
    public float ProyectileSpeed;
}
