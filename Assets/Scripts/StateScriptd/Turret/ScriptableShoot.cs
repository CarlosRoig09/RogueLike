using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableShoot", menuName = "ScriptableShoot")]
public class ScriptableShoot : ScriptableAction
{
    public ShootControler ShootControler;
    public override void OnFinishedState()
    {
    }

    public override void OnSetState()
    {
    }

    public override void OnUpdate()
    {
        ShootControler.ProyectileSpawn();
    }
}
