using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaponAttack", menuName = "WeaponAttack")]
public  class WeaponAttack : ScriptableObject
{
    //public Animation Attack;
    public GameObject ParticleSystem;
    public AudioClip AttackSound;
    public float CountDown;
    public float Damage;
    public float ImpulseForce;
}


