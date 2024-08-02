using Game.Scripts.Scenes.GameScene.Behaviour.States;
using Tools.Runtime.StateBehaviour;
using Zenject;

namespace Game.Scripts.Scenes.GameScene.Behaviour
{
    public class GameplayLoopStateManager : StateManager<GameState>
    {

        [Inject]
        public GameplayLoopStateManager(DiContainer container) : base(new []
        {
            typeof(PlayerInteractionState),
            typeof(ShuffleCardsState),
            typeof(PreparationPhaseState),
            typeof(ShowCardsState),
            typeof(WinState),
            typeof(LoseState)
        }, container)
        {
        }
    }
}