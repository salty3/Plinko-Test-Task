using Zenject;

namespace Game.Scripts.Gameplay.States
{
    public class ShuffleCardsState : GameState
    {
        private CardsFieldPresenter _cardsFieldPresenter;
        private GameplayLoopStateManager _gameplayLoopStateManager;

        [Inject]
        public ShuffleCardsState(CardsFieldPresenter cardsFieldPresenter, GameplayLoopStateManager gameplayLoopStateManager)
        {
            _cardsFieldPresenter = cardsFieldPresenter;
            _gameplayLoopStateManager = gameplayLoopStateManager;
        }

        public override void Initialize()
        {
            _cardsFieldPresenter.Shuffle();
            _gameplayLoopStateManager.SwitchToState<PlayerInteractionState>();
        }

        public override void Dispose()
        {
        }
    }
}