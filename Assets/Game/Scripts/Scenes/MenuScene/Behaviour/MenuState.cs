using Tools.Runtime.StateBehaviour;

namespace Game.Scripts.Scenes.MenuScene.Behaviour
{
    public abstract class MenuState : IState
    {
        public abstract void Initialize();
        public abstract void Dispose();
    }
}