using System.Linq;
using Cysharp.Threading.Tasks;
using Game.Scripts.ApplicationCore;
using Game.Scripts.LevelsSystem;
using Game.Scripts.LevelsSystem.Levels;
using Game.Scripts.Scenes.GameScene.UI.WinScreen;
using Game.Scripts.TimerSystem;
using Tools.SceneManagement.Runtime;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game.Scripts.Scenes.GameScene.Behaviour.States
{
    public class WinState : GameState
    {
        private readonly WinScreenPresenter _winScreenPresenter;
        private readonly SceneReferences _sceneReferences;
        private readonly SceneController _sceneController;
        private readonly IReadOnlyLevelEntity _levelEntity;
        private readonly ILevelsService _levelsService;
        private readonly Timer _timer;
        

        [Inject]
        public WinState(WinScreenPresenter winScreenPresenter, 
            SceneReferences sceneReferences,
            SceneController sceneController, 
            IReadOnlyLevelEntity levelEntity, 
            ILevelsService levelsService, 
            Timer timer)
        {
            _winScreenPresenter = winScreenPresenter;
            _sceneReferences = sceneReferences;
            _sceneController = sceneController;
            _levelEntity = levelEntity;
            _levelsService = levelsService;
            _timer = timer;
        }

        public override void Initialize()
        {
            _timer.Dispose();
            _winScreenPresenter.ToMainMenuClicked.AddListener(OnToMainMenuClicked);
            _winScreenPresenter.PlayNextLevelClicked.AddListener(OnPlayNextLevelClicked);
            _winScreenPresenter.Show();
            _winScreenPresenter.SetPlayedTime(_timer.ElapsedTime);

            var nextLevelData = _levelsService.GetLevelData(_levelEntity.LevelIndex + 1);
            if (nextLevelData == null)
            {
                _winScreenPresenter.HidePlayNextLevelButton();
            }
        }
        
        public override void Dispose()
        {
            _winScreenPresenter.ToMainMenuClicked.RemoveListener(OnToMainMenuClicked);
            _winScreenPresenter.PlayNextLevelClicked.RemoveListener(OnPlayNextLevelClicked);
            _winScreenPresenter.Hide();
        }
        
        private void OnPlayNextLevelClicked()
        {
            var nextLevelEntity = _levelsService.Levels.ElementAtOrDefault(_levelEntity.LevelIndex + 1);
            
            var builder = _sceneReferences.GameScene
                .LoadScene()
                .WithLoadingScreen(_sceneReferences.Loading)
                .WithMode(LoadSceneMode.Additive)
                .WithClosing(_sceneReferences.GameScene)
                .WithRegistrations(container => container.BindInstance(nextLevelEntity));
            
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