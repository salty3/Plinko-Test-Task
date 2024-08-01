using System.Linq;
using Game.Scripts.Gameplay;
using Game.Scripts.Gameplay.InfoPanel;
using Zenject;

namespace Game.Scripts.GameScene.Gameplay.Behaviour.States
{
    public class PlayerInteractionState : GameState
    {
        private readonly CardsFieldPresenter _cardsFieldPresenter;
        private readonly GameplayLoopStateManager _gameplayLoopStateManager;
        

        private int _matchesCount;
        private readonly int _maxMatchesCount;

        private int _mismatchesCount;
        private readonly int _maxMismatchesCount;

        private readonly InfoPanelPresenter _infoPanelPresenter;
        
        [Inject]
        public PlayerInteractionState(CardsFieldPresenter cardsFieldPresenter, GameplayLoopStateManager gameplayLoopStateManager, LevelData levelData, InfoPanelPresenter infoPanelPresenter)
        {
            _cardsFieldPresenter = cardsFieldPresenter;
            _gameplayLoopStateManager = gameplayLoopStateManager;
            _infoPanelPresenter = infoPanelPresenter;
            _maxMatchesCount = levelData.Cards.Count();
            _maxMismatchesCount = levelData.MaxMismatchCount;
        }
        
        public override void Initialize()
        {
            _cardsFieldPresenter.Matched.AddListener(OnMatch);
            _cardsFieldPresenter.Mismatched.AddListener(OnMismatch);
            _infoPanelPresenter.ShuffleButtonClicked.AddListener(OnShuffleButtonClicked);
            
            _cardsFieldPresenter.UnblockInteraction();
        }

        public override void Dispose()
        {
            _cardsFieldPresenter.BlockInteraction();
            
            _infoPanelPresenter.ShuffleButtonClicked.RemoveListener(OnShuffleButtonClicked);
            _cardsFieldPresenter.Matched.RemoveListener(OnMatch);
            _cardsFieldPresenter.Mismatched.RemoveListener(OnMismatch);
        }
        
        private void OnShuffleButtonClicked()
        {
            _gameplayLoopStateManager.SwitchToState<ShuffleCardsState>();
        }
        
        private void OnMatch(string pairId)
        {
            _matchesCount++;
            _cardsFieldPresenter.CompletePair(pairId);
            _infoPanelPresenter.SetMatchCount(_matchesCount);
            if (_matchesCount == _maxMatchesCount)
            {
                _gameplayLoopStateManager.SwitchToState<WinState>();
            }
        }
        
        private void OnMismatch()
        {
            _mismatchesCount++;
            _infoPanelPresenter.SetMismatchCount(_mismatchesCount);
            if (_mismatchesCount == _maxMismatchesCount)
            {
                _gameplayLoopStateManager.SwitchToState<LoseState>();
            }
        }
    }
}