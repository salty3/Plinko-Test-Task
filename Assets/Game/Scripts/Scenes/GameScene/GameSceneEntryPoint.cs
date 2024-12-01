using System;
using Cysharp.Threading.Tasks;
using Game.Scripts.ApplicationCore.ApplicationInitialization;
using Game.Scripts.Core;
using Tools.Runtime;
using Tools.SceneManagement.Runtime;
using UnityEngine;
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
            _sceneContext.Container.Resolve<PlinkoCore>().Initialize();
            progress.Report(1f);
        }
    }
}