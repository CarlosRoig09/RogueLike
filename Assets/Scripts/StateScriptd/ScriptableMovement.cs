
using UnityEngine;
[CreateAssetMenu(fileName = "ScriptableMovement", menuName = "ScriptableMovement")]
public class ScriptableMovement : ScriptableAction
{
    public Invulnerability damage;
    public Rigidbody2D rb;
    public float directionX;
    public float directionY;
    public float speed;
    public override void OnSetState()
    {
        damage = Invulnerability.Damagable;
    }

    public override void OnFinishedState()
    {
        rb.velocity = Vector3.zero;
    }

    public override void OnUpdate()
    {
        rb.velocity = new Vector3(directionX * speed * Time.fixedDeltaTime, directionY * speed * Time.fixedDeltaTime);
    }
}
