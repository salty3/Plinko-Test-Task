using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Scripts.ApplicationCore.ApplicationEntry;
using Tools.Runtime;
using Tools.SceneManagement.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game.Scripts.ApplicationCore.ApplicationInitialization
{
    //Any heavy loading here
    public class ApplicationInitializer : CachedMonoBehaviour, ISceneEntryPoint
    {
        [SerializeField] private SceneContext _sceneContext;
        
        private IEnumerable<IApplicationLifetimeHandler> _applicationLifetimeHandlers;
        private IEnumerable<IService> _services;
        
        private SceneReferences _sceneRefs;
        private SceneController _sceneController;
        
        private void Resolve(DiContainer container)
        {
            _sceneController = container.Resolve<SceneController>();
            _sceneRefs = container.Resolve<SceneReferences>();
            _applicationLifetimeHandlers = container.Resolve<IEnumerable<IApplicationLifetimeHandler>>();
            _services = container.Resolve<IEnumerable<IService>>();
        }
        
        public async UniTask OnSceneOpen(IProgress<float> progress)
        {
            var token = DestroyCancellationToken;
        
            Application.targetFrameRate = 60;
            
            _sceneContext.ContractNames = new[] { nameof(ApplicationInitializer) };
            _sceneContext.ParentContractNames = new[] { nameof(Startup) };
            _sceneContext.Run();
            Resolve(_sceneContext.Container);

            await InitializeServices(progress, token);
            await LoadFirstScene();
            progress.Report(1f);
        }

        private async UniTask LoadFirstScene()
        {
            var builder = _sceneRefs.GameScene
                .LoadScene()
                .WithMode(LoadSceneMode.Additive);

            await _sceneController.LoadAsync(builder);
        }

        private async UniTask InitializeServices(IProgress<float> progress, CancellationToken token)
        {
            float progressPerInit = 0.9f / _services.Count();
            float currentProgress = 0f;
            
            foreach (var service in _services)
            {
                await service.Initialize(token);
                progress.Report(currentProgress += progressPerInit);
            }
        }
        
        private void OnApplicationFocus(bool hasFocus)
        {
            foreach (var handler in _applicationLifetimeHandlers)
            {
                handler.OnApplicationFocus(hasFocus);
            }
        }
        
        private void OnApplicationQuit()
        {
            foreach (var handler in _applicationLifetimeHandlers)
            {
                handler.OnApplicationQuit();
            }
        }
    }
}