using UnityEngine;

public interface IWeaponControler 
{
    public WeaponData WeaponSO { get; set; }
    public abstract void SecondButtonAttack();

    public abstract void FirstButtonAttack();

    public abstract void PushOtherGO(Collider2D collision, float impulse);

    public abstract void CollisionEnable();

    public abstract void CollisionDisable();
}
