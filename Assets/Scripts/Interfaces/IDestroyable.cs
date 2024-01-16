public interface IDestroyable
{
    public abstract void GetHitByPlayer(float damage,float time);
    public abstract void Destroyed();
    public abstract bool DropAnObject();
}
