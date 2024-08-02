using System;
using Cysharp.Threading.Tasks;
using Game.Scripts.Gameplay;
using Zenject;

namespace Game.Scripts.GameScene.Gameplay.Behaviour.States
{
    public class ShowCardsState : GameState
    {
        private readonly CardsFieldPresenter _cardsFieldPresenter;
        private readonly GameplayLoopStateManager _gameplayLoopStateManager;
        
        private readonly TimeSpan _showCardsTime = TimeSpan.FromSeconds(3f);
        
        [Inject]
        public ShowCardsState(CardsFieldPresenter cardsFieldPresenter, GameplayLoopStateManager gameplayLoopStateManager)
        {
            _cardsFieldPresenter = cardsFieldPresenter;
            _gameplayLoopStateManager = gameplayLoopStateManager;
        }

        public override void Initialize()
        {
            _cardsFieldPresenter.BlockInteraction();
            ShowCards().Forget();
        }

        public override void Dispose()
        {
            _cardsFieldPresenter.UnblockInteraction();
        }
        
        private async UniTask ShowCards()
        {
            await _cardsFieldPresenter.ShowCardsFor(_showCardsTime);
            _gameplayLoopStateManager.SwitchToState<PlayerInteractionState>();
        }
    }
}