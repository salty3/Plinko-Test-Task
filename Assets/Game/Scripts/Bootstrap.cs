using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Tools.Runtime;
using Tools.SceneManagement.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game.Scripts
{
    //Any heavy loading here
    public class Bootstrap : CachedMonoBehaviour, ISceneEntryPoint
    {
        [SerializeField] private SceneContext _sceneContext;
        
        private IEnumerable<IApplicationLifetimeHandler> _applicationLifetimeHandlers;
        private IEnumerable<IService> _services;
        private IEnumerable<IEverySecondTickable> _everySecondTickables;
        
        private SceneReferences _sceneRefs;
        private SceneController _sceneController;
        
        private void Resolve(DiContainer container)
        {
            _sceneController = container.Resolve<SceneController>();
            _sceneRefs = container.Resolve<SceneReferences>();
            _applicationLifetimeHandlers = container.Resolve<IEnumerable<IApplicationLifetimeHandler>>();
            _services = container.Resolve<IEnumerable<IService>>();
            _everySecondTickables = container.Resolve<IEnumerable<IEverySecondTickable>>();
        }
        
        public async UniTask OnSceneOpen(IProgress<float> progress)
        {
            var token = DestroyCancellationToken;
        
            Application.targetFrameRate = 60;
            
            _sceneContext.ContractNames = new[] { nameof(Bootstrap) };
            _sceneContext.ParentContractNames = new[] { nameof(Startup) };
            _sceneContext.Run();
            Resolve(_sceneContext.Container);
            
            float progressPerInit = 0.9f / _services.Count();
            float currentProgress = 0f;
            
            
            foreach (var service in _services)
            {
                await service.Initialize(token);
                progress.Report(currentProgress += progressPerInit);
            }
            
            var builder = _sceneRefs.GameScene
                .LoadScene()
                .WithMode(LoadSceneMode.Additive);
            
            await _sceneController.LoadAsync(builder);
            progress.Report(1f);
        }
        
        private void OnApplicationFocus(bool hasFocus)
        {
            /*foreach (var handler in _applicationLifetimeHandlers)
            {
                handler.OnApplicationFocus(hasFocus);
            }*/
        }
        
        private void OnApplicationQuit()
        {
            /*foreach (var handler in _applicationLifetimeHandlers)
            {
                handler.OnApplicationQuit();
            }*/
        }
        
        private async UniTask EverySecondTick()
        {
            var token = DestroyCancellationToken;
            while (!token.IsCancellationRequested)
            {
                foreach (var tickable in _everySecondTickables)
                {
                    tickable.OnEverySecondTick();
                }
                await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: token);
            }
        }
    }
}