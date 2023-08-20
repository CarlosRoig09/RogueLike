using UnityEngine;

public interface IWeaponControler 
{
    public WeaponData WeaponSO { get; set; }

    public float WeaponDamage { get; set; }

    public float WeaponSpeed { get; set; }

    public float ProyectileDamage { get; set; }
    public float ProyectileSpeed { get; set;}
    public abstract void SecondButtonAttack();

    public abstract void FirstButtonAttack();

    public abstract void PushOtherGO(Collider2D collision, float impulse);

    public abstract void CollisionEnable();

    public abstract void CollisionDisable();

    public void InvoqueParticle(/*GameObject particle, Vector3 particlePosition, Quaternion particleRotation*/)
    {

    }
}
