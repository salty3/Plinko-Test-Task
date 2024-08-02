using Cysharp.Threading.Tasks;
using Game.Scripts.FieldSystem;
using Game.Scripts.Gameplay;
using Game.Scripts.TimerSystem;
using Zenject;

namespace Game.Scripts.GameScene.Gameplay.Behaviour.States
{
    public class PreparationPhaseState : GameState
    {
        private readonly IReadOnlyLevelEntity _levelEntity;
        private readonly CardsFieldPresenter _cardsFieldPresenter;
        private readonly GameplayLoopStateManager _gameplayLoopStateManager;
        private readonly ILevelsService _levelsService;
        private readonly Timer _timer;

        [Inject]
        public PreparationPhaseState(IReadOnlyLevelEntity levelEntity,
            CardsFieldPresenter cardsFieldPresenter,
            GameplayLoopStateManager gameplayLoopStateManager,
            ILevelsService levelsService, 
            Timer timer)
        {
            _levelEntity = levelEntity;
            _cardsFieldPresenter = cardsFieldPresenter;
            _gameplayLoopStateManager = gameplayLoopStateManager;
            _levelsService = levelsService;
            _timer = timer;
        }

        public override void Initialize()
        {
            InitializeAsync().Forget();
        }

        private async UniTask InitializeAsync()
        {
            _levelsService.CreateField(_levelEntity.LevelIndex, out bool newField);
            await _cardsFieldPresenter.SetField(_levelEntity.Field);
            _cardsFieldPresenter.BlockInteraction();
            if (newField)
            {
                _gameplayLoopStateManager.SwitchToState<ShuffleCardsState>();
            }
            else
            {
                _gameplayLoopStateManager.SwitchToState<ShowCardsState>();
            }
            _timer.Start();
        }

        public override void Dispose()
        {
            _cardsFieldPresenter.UnblockInteraction();
        }
    }
}