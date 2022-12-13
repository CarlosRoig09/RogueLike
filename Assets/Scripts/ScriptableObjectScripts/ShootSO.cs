using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProyectileUser
{
    Player,
    Enemy
}
[CreateAssetMenu(fileName = "ShootSO", menuName = "ShootSO")]
public class ShootSO : ScriptableObject
{
    public GameObject Proyectile;
    public float ProyectileDamage;
    public float ProyectileSpeed;
    public float BulletsXShoot;
    public float currentBullets;
    public float maxDistance;
    public float currentDistance;
    public float TotalBullets;
    public float CadenceTime;
    public float CountCadenceTime;
    public float Reload;
    public ProyectileUser proyectileUser;
}
