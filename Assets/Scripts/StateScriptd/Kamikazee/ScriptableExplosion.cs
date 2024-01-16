using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableExplosion", menuName = "ScriptableExplosion")]
public class ScriptableExplosion : ScriptableAction
{
    public float Timer;
    private float _countTillDisapear;
    public delegate void Explosion();
    public event Explosion OnExplosion;
    public override void OnSetState()
    {
        _countTillDisapear= 0;
    }

    public override void OnFinishedState()
    {

    }

    public override void OnUpdate()
    {
        if (Timer > _countTillDisapear)
            _countTillDisapear += Time.deltaTime;
        else
        {
           OnExplosion();
        }
    }
}
