namespace Game.Scripts.ApplicationCore
{
    public interface IDeepCloneable<out T>
    {
        T DeepClone();
    }
}