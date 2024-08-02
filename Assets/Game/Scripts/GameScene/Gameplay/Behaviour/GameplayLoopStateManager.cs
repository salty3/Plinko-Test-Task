using Game.Scripts.GameScene.Gameplay.Behaviour.States;
using Tools.Runtime.StateBehaviour;
using Zenject;

namespace Game.Scripts.GameScene.Gameplay.Behaviour
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