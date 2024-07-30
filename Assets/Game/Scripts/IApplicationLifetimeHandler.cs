namespace Game.Scripts
{
    public interface IApplicationLifetimeHandler
    {
        void OnApplicationFocus(bool hasFocus);
        void OnApplicationQuit();
    }
}