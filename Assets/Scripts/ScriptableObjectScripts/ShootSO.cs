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
    public float TotalBullets;
    public float currentBullets;
    public float maxDistance;
    public float CadenceTime;
    public float Reload;
    public float ImpulseForce;
    public ProyectileUser proyectileUser;
    public bool rangeAttack;
}




