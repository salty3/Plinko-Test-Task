using Cysharp.Threading.Tasks;
using Game.Scripts.Gameplay;
using Tools.SceneManagement.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game.Scripts.MenuScene
{
    public class SelectLevelState : MenuState
    {
        private readonly LevelsCollection _levelsCollection;
        private readonly SelectLevelScreenView _view;
        private readonly MenuStateManager _menuStateManager;
        private readonly SceneController _sceneController;
        private readonly SceneReferences _sceneReferences;

        [Inject]
        public SelectLevelState(LevelsCollection levelsCollection,
            SelectLevelScreenView view, 
            MenuStateManager menuStateManager,
            SceneController sceneController, 
            SceneReferences sceneReferences)
        {
            _levelsCollection = levelsCollection;
            _view = view;
            _menuStateManager = menuStateManager;
            _sceneController = sceneController;
            _sceneReferences = sceneReferences;
        }
        
        private void OnBackButtonClicked()
        {
            _menuStateManager.SwitchToState<MainMenuState>();
        }
        
        private async UniTask LoadLevel(LevelData levelData)
        {
            var builder = _sceneReferences.GameScene.LoadScene()
                .WithLoadingScreen(_sceneReferences.Loading)
                .WithMode(LoadSceneMode.Additive)
                .WithRegistrations(container => container.BindInstance(levelData).AsSingle());

            await _sceneController.LoadAsync(builder);
            _sceneController.Close(_sceneReferences.MainMenu);
        }

        public override void Initialize()
        {
            foreach (var levelData in _levelsCollection.Levels)
            {
                var button = _view.CreateLevelButton(levelData);
                button.Button.onClick.AddListener(() => LoadLevel(levelData).Forget());
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