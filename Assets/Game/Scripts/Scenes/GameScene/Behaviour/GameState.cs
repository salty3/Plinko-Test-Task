using Tools.Runtime.StateBehaviour;

namespace Game.Scripts.Scenes.GameScene.Behaviour
{
    public abstract class GameState : IState
    {
        public abstract void Initialize();
        public abstract void Dispose();
    }
}