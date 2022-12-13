public interface IDestroyable
{
    public abstract void GetHitByPlayer(float damage);
    public abstract void Destroyed();
    public abstract bool DropAnObject();
}
