using Tools.Runtime.StateBehaviour;

namespace Game.Scripts.Gameplay
{
    public abstract class GameState : IState
    {
        public abstract void Initialize();
        public abstract void Dispose();
    }
}