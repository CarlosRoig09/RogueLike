using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

[CreateAssetMenu(fileName = "ScriptableDash", menuName = "ScriptableDash")]
public class ScriptableDash : ScriptableAction
{
    public Vector3 position;
    public float dashSpeed;
    public float dashDuration;
    public Rigidbody2D rb;
    public Invulnerability Damagable;
    public ControlStats controlStats;
    public override void OnSetState()
    {
        var vectorPM = Camera.main.ScreenToWorldPoint(Input.mousePosition) - position;
        rb.AddForce(vectorPM.normalized * dashSpeed, ForceMode2D.Impulse);
        Damagable = Invulnerability.NoDamagable;
        controlStats.ModificadorDeStat(Type.WeaponsDamage, 0.5f, dashDuration);
    }

    public override void OnFinishedState()
    {
        Damagable = Invulnerability.Damagable;
        rb.velocity = Vector3.zero;
    }

    public override void OnUpdate()
    {
    }
}
