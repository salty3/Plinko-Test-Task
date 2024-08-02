namespace Game.Scripts.ApplicationCore
{
    public interface IApplicationLifetimeHandler
    {
        void OnApplicationFocus(bool hasFocus);
        void OnApplicationQuit();
    }
}