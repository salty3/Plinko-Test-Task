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
    public class Bootstrap : CachedMonoBehaviour, ISceneEntryPoint
    {
        [SerializeField] private SceneContext _sceneContext;
        
        private IEnumerable<IApplicationLifetimeHandler> _applicationLifetimeHandlers;
        private IEnumerable<IService> _services;
        private IEnumerable<IEverySecondTickable> _everySecondTickables;
        
        private SceneReferences _sceneRefs;
        
        private void Resolve(DiContainer container)
        {
            _sceneRefs = container.Resolve<SceneReferences>();
            _applicationLifetimeHandlers = container.Resolve<IEnumerable<IApplicationLifetimeHandler>>();
            _services = container.Resolve<IEnumerable<IService>>();
            _everySecondTickables = container.Resolve<IEnumerable<IEverySecondTickable>>();
        }
        
        public async UniTask OnSceneOpen(IProgress<float> progress)
        {
            var token = DestroyCancellationToken;
        
            Application.targetFrameRate = 60;
            _sceneContext.Run();
            Resolve(_sceneContext.Container);
            
            float progressPerInit = 0.9f / _services.Count();
            float currentProgress = 0f;
            
            //SoundManager.Init();
            foreach (var service in _services)
            {
                await service.Initialize(token);
                progress.Report(currentProgress += progressPerInit);
            }
            
            await _sceneRefs.GameScene
                .LoadScene()
                .WithMode(LoadSceneMode.Additive)
                .Execute();
        
            progress.Report(1f);
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