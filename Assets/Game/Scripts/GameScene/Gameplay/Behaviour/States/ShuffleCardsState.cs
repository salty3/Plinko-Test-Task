using System;
using Cysharp.Threading.Tasks;
using Game.Scripts.Gameplay;
using Zenject;

namespace Game.Scripts.GameScene.Gameplay.Behaviour.States
{
    public class ShuffleCardsState : GameState
    {
        private CardsFieldPresenter _cardsFieldPresenter;
        private GameplayLoopStateManager _gameplayLoopStateManager;

        private readonly TimeSpan _showCardsTime = TimeSpan.FromSeconds(3f);

        [Inject]
        public ShuffleCardsState(CardsFieldPresenter cardsFieldPresenter, GameplayLoopStateManager gameplayLoopStateManager)
        {
            _cardsFieldPresenter = cardsFieldPresenter;
            _gameplayLoopStateManager = gameplayLoopStateManager;
        }

        public override void Initialize()
        {
            _cardsFieldPresenter.BlockInteraction();
            ShuffleCards()
                .ContinueWith(() => _gameplayLoopStateManager.SwitchToState<PlayerInteractionState>())
                .Forget();
        }

        private async UniTask ShuffleCards()
        {
            await _cardsFieldPresenter.Shuffle();
            await _cardsFieldPresenter.ShowCardsFor(_showCardsTime);
        }

        public override void Dispose()
        {
            _cardsFieldPresenter.UnblockInteraction();
        }
    }
}