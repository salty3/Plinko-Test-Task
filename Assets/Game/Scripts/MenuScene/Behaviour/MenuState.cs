using Game.Scripts.Gameplay;
using Tools.Runtime.StateBehaviour;

namespace Game.Scripts.MenuScene
{
    public abstract class MenuState : IState
    {
        public abstract void Initialize();
        public abstract void Dispose();
    }
}