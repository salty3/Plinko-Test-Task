using System;
using Tools.Runtime.StateBehaviour;
using Zenject;

namespace Game.Scripts.Scenes.GameScene.Behaviour
{
    public class GameplayLoopStateManager : StateManager<GameState>
    {

        /*[Inject]
        public GameplayLoopStateManager(DiContainer container) : base(new []
        {
           
        }, container)
        {
        }*/
        public GameplayLoopStateManager(Type[] types, DiContainer container) : base(types, container)
        {
        }
    }
}