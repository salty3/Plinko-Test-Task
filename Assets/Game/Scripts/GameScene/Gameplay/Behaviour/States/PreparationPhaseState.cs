using Zenject;

namespace Game.Scripts.Gameplay.States
{
    public class PreparationPhaseState : GameState
    {
        private LevelData _levelData;
        private CardsFieldPresenter _cardsFieldPresenter;

        private GameplayLoopStateManager _gameplayLoopStateManager;

        [Inject]
        public PreparationPhaseState(LevelData levelData, CardsFieldPresenter cardsFieldPresenter, GameplayLoopStateManager gameplayLoopStateManager)
        {
            _levelData = levelData;
            _cardsFieldPresenter = cardsFieldPresenter;
            _gameplayLoopStateManager = gameplayLoopStateManager;
        }

        public override void Initialize()
        {
            _cardsFieldPresenter.SetLevel(_levelData);
            _gameplayLoopStateManager.SwitchToState<ShuffleCardsState>();
        }

        public override void Dispose()
        {
        }
    }
}