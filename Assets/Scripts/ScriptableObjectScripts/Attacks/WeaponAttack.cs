using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponAttack : ScriptableObject
{
    //public Animation Attack;
    public GameObject ParticleSystem;
    public AudioClip AttackSound;
    public float CountDown;
    public float Damage;
    public float ImpulseForce;

    public abstract void Attack();
}


