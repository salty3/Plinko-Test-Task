using System;
using System.Linq;
using Game.Scripts.FieldSystem;
using Game.Scripts.Gameplay;
using Game.Scripts.Gameplay.InfoPanel;
using Zenject;

namespace Game.Scripts.GameScene.Gameplay.Behaviour.States
{
    public class PlayerInteractionState : GameState
    {
        private readonly IReadOnlyLevelEntity _levelEntity;
        private readonly CardsFieldPresenter _cardsFieldPresenter;
        private readonly GameplayLoopStateManager _gameplayLoopStateManager;
        

        private int _matchesCount;
        private readonly int _maxMatchesCount;

        private int _mismatchesCount;
        private readonly int _maxMismatchesCount;

        private readonly InfoPanelPresenter _infoPanelPresenter;

        private readonly ILevelsService _levelsService;
        
        [Inject]
        public PlayerInteractionState(IReadOnlyLevelEntity levelEntity, CardsFieldPresenter cardsFieldPresenter, GameplayLoopStateManager gameplayLoopStateManager, InfoPanelPresenter infoPanelPresenter, ILevelsService levelsService)
        {
            _levelEntity = levelEntity;
            _cardsFieldPresenter = cardsFieldPresenter;
            _gameplayLoopStateManager = gameplayLoopStateManager;
            _infoPanelPresenter = infoPanelPresenter;
            _levelsService = levelsService;
            var levelData = _levelsService.GetLevelData(levelEntity.LevelIndex);
            _maxMatchesCount = levelData.Cards.Count();
            _maxMismatchesCount = levelData.MaxMismatchCount;
            _matchesCount = levelEntity.MatchesCount;
            _mismatchesCount = levelEntity.MismatchesCount;
        }
        
        public override void Initialize()
        {
            _cardsFieldPresenter.Matched.AddListener(OnMatch);
            _cardsFieldPresenter.Mismatched.AddListener(OnMismatch);
            _infoPanelPresenter.ShuffleButtonClicked.AddListener(OnShuffleButtonClicked);
            
            _cardsFieldPresenter.UnblockInteraction();
            
            //There should be some reactive way to update the UI
            _infoPanelPresenter.SetMatchCount(_matchesCount);
            _infoPanelPresenter.SetMismatchCount(_mismatchesCount);
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
            
            _levelsService.AddMatch(_levelEntity.LevelIndex);
            _levelsService.CompletePair(_levelEntity.LevelIndex, pairId);
            _cardsFieldPresenter.CompletePair(pairId);
            
            
            _infoPanelPresenter.SetMatchCount(_matchesCount);
            if (_matchesCount == _maxMatchesCount)
            {
                _levelsService.SetAsCompleted(_levelEntity.LevelIndex);
                _gameplayLoopStateManager.SwitchToState<WinState>();
            }
        }
        
        private void OnMismatch()
        {
            _mismatchesCount++;
            
            _levelsService.AddMismatch(_levelEntity.LevelIndex);
            
            _infoPanelPresenter.SetMismatchCount(_mismatchesCount);
            if (_mismatchesCount >= _maxMismatchesCount)
            {
                _gameplayLoopStateManager.SwitchToState<LoseState>();
            }
        }
    }
}