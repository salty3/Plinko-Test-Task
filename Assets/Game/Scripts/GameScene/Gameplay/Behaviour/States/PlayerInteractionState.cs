using UnityEngine;
using Zenject;

namespace Game.Scripts.Gameplay.States
{
    public class PlayerInteractionState : GameState
    {
        private CardsFieldPresenter _cardsFieldPresenter;
        private GameplayLoopStateManager _gameplayLoopStateManager;
        
        int _matchesCount = 0;
        
        [Inject]
        public PlayerInteractionState(CardsFieldPresenter cardsFieldPresenter, GameplayLoopStateManager gameplayLoopStateManager)
        {
            _cardsFieldPresenter = cardsFieldPresenter;
            _gameplayLoopStateManager = gameplayLoopStateManager;
        }
        
        public override void Initialize()
        {
            _cardsFieldPresenter.Matched.AddListener(OnMatch);
            _cardsFieldPresenter.Mismatched.AddListener(OnMissmatch);
            
            _cardsFieldPresenter.UnblockInteraction();
        }

        public override void Dispose()
        {
            _cardsFieldPresenter.BlockInteraction();
            
            _cardsFieldPresenter.Matched.RemoveListener(OnMatch);
            _cardsFieldPresenter.Mismatched.RemoveListener(OnMissmatch);
        }
        
        private void OnMatch()
        {
            _matchesCount++;
            Debug.Log(_matchesCount);
        }
        
        private void OnMissmatch()
        {
            Debug.Log("Missmatch");
        }
    }
}