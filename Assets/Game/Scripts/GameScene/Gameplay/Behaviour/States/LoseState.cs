using Cysharp.Threading.Tasks;
using Game.Scripts.FieldSystem;
using Game.Scripts.Gameplay;
using Game.Scripts.GameScene.Gameplay.WinScreen;
using Game.Scripts.TimerSystem;
using Tools.SceneManagement.Runtime;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game.Scripts.GameScene.Gameplay.Behaviour.States
{
    public class LoseState : GameState
    {
        private readonly LoseScreenPresenter _loseScreenPresenter;
        private readonly SceneReferences _sceneReferences;
        private readonly SceneController _sceneController;
        private readonly IReadOnlyLevelEntity _levelEntity;
        private readonly ILevelsService _levelsService;
        private readonly Timer _timer;
        

        [Inject]
        public LoseState(LoseScreenPresenter loseScreenPresenter,
            SceneReferences sceneReferences,
            SceneController sceneController, 
            IReadOnlyLevelEntity levelEntity, 
            ILevelsService levelsService, 
            Timer timer)
        {
            _loseScreenPresenter = loseScreenPresenter;
            _sceneReferences = sceneReferences;
            _sceneController = sceneController;
            _levelEntity = levelEntity;
            _levelsService = levelsService;
            _timer = timer;
        }

        public override void Initialize()
        {
            _timer.Dispose();
            _loseScreenPresenter.Show();
            _loseScreenPresenter.RetryLevelClicked.AddListener(OnRetryLevelClicked);
            _loseScreenPresenter.ToMainMenuClicked.AddListener(OnToMainMenuClicked);
            _levelsService.ResetLevel(_levelEntity.LevelIndex);
        }

        public override void Dispose()
        {
            _loseScreenPresenter.RetryLevelClicked.RemoveListener(OnRetryLevelClicked);
            _loseScreenPresenter.ToMainMenuClicked.RemoveListener(OnToMainMenuClicked);
            _loseScreenPresenter.Hide();
        }
        
        private void OnRetryLevelClicked()
        {
            var builder = _sceneReferences.GameScene
                .LoadScene()
                .WithLoadingScreen(_sceneReferences.Loading)
                .WithMode(LoadSceneMode.Additive)
                .WithClosing(_sceneReferences.GameScene)
                .WithRegistrations(container => container.BindInstance(_levelEntity));
            
            _sceneController.LoadAsync(builder).Forget();
        }
        
        private void OnToMainMenuClicked()
        {
            ToMainMenuAsync().Forget();
        }

        private async UniTask ToMainMenuAsync()
        {
            var builder = _sceneReferences.MainMenu
                .LoadScene()
                .WithLoadingScreen(_sceneReferences.Loading)
                .WithClosing(_sceneReferences.GameScene)
                .WithMode(LoadSceneMode.Additive);

            await _sceneController.LoadAsync(builder);
        }
    }
}