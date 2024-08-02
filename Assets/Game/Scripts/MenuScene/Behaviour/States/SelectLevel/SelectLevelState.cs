using Cysharp.Threading.Tasks;
using Game.Scripts.FieldSystem;
using Game.Scripts.Gameplay;
using Game.Scripts.GameScene.Gameplay;
using Tools.SceneManagement.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game.Scripts.MenuScene
{
    public class SelectLevelState : MenuState
    {
        private readonly ILevelsService _levelsService;
        private readonly SelectLevelScreenView _view;
        private readonly MenuStateManager _menuStateManager;
        private readonly SceneController _sceneController;
        private readonly SceneReferences _sceneReferences;

        [Inject]
        public SelectLevelState(ILevelsService levelsService,
            SelectLevelScreenView view, 
            MenuStateManager menuStateManager,
            SceneController sceneController, 
            SceneReferences sceneReferences)
        {
            _levelsService = levelsService;
            _view = view;
            _menuStateManager = menuStateManager;
            _sceneController = sceneController;
            _sceneReferences = sceneReferences;
        }
        
        private void OnBackButtonClicked()
        {
            _menuStateManager.SwitchToState<MainMenuState>();
        }
        
        private async UniTask LoadLevel(IReadOnlyLevelEntity levelEntity)
        {
            var builder = _sceneReferences.GameScene.LoadScene()
                .WithLoadingScreen(_sceneReferences.Loading)
                .WithMode(LoadSceneMode.Additive)
                .WithClosing(_sceneReferences.MainMenu)
                .WithRegistrations(container => container.BindInstance(levelEntity).AsSingle());

            await _sceneController.LoadAsync(builder);
        }

        public override void Initialize()
        {
            //Inefficient workaround :)
            foreach (var levelEntity in _levelsService.Levels)
            {
                var levelData = _levelsService.GetLevelData(levelEntity.LevelIndex);
                var button = _view.CreateLevelButton(levelData);
                if (levelEntity.IsCompleted)
                {
                    button.SetAsCompleted();
                }
                button.Button.onClick.AddListener(() => LoadLevel(levelEntity).Forget());
            }
            
            _view.BackButton.onClick.AddListener(OnBackButtonClicked);
            _view.Show();
        }

        public override void Dispose()
        {
            _view.BackButton.onClick.RemoveListener(OnBackButtonClicked);
            _view.Hide();
            _view.Clear();
        }
    }
}