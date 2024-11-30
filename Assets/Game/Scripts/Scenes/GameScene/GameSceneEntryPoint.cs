using System;
using Cysharp.Threading.Tasks;
using Game.Scripts.ApplicationCore;
using Game.Scripts.ApplicationCore.ApplicationInitialization;
using Game.Scripts.Scenes.GameScene.Behaviour;
using Game.Scripts.Scenes.GameScene.Behaviour.States;
using Tools.Runtime;
using Tools.SceneManagement.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game.Scripts.Scenes.GameScene
{
    public class GameSceneEntryPoint : CachedMonoBehaviour, ISceneEntryPoint
    {
        [SerializeField] private SceneContext _sceneContext;
        
        //Can be used for more control over scene initialization process, heavy loadings etc.
        //Loading screen hides only after this method is completed.
        public async UniTask OnSceneOpen(IProgress<float> progress)
        {
            _sceneContext.ParentContractNames = new[] { nameof(ApplicationInitializer) };
            _sceneContext.Run();
            await UniTask.NextFrame();
            _sceneContext.Container.Resolve<GameplayLoopStateManager>().SwitchToState<PlayState>();
            
            
            progress.Report(1f);
        }
        
        private void ReturnToMenu()
        {
            var sceneController = _sceneContext.Container.Resolve<SceneController>();
            var sceneReferences = _sceneContext.Container.Resolve<SceneReferences>();

            var builder = sceneReferences.MainMenu.LoadScene()
                .WithLoadingScreen(sceneReferences.Loading)
                .WithMode(LoadSceneMode.Additive)
                .WithClosing(sceneReferences.GameScene);
            
            sceneController.LoadAsync(builder).Forget();
        }
    }
}