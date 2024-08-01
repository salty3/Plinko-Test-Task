using System;
using Cysharp.Threading.Tasks;
using Game.Scripts.Gameplay;
using Game.Scripts.GameScene.Gameplay.Behaviour;
using Game.Scripts.GameScene.Gameplay.Behaviour.States;
using Tools.Runtime;
using Tools.SceneManagement.Runtime;
using UnityEngine;
using Zenject;

namespace Game.Scripts
{
    public class GameSceneEntryPoint : CachedMonoBehaviour, ISceneEntryPoint
    {
        [SerializeField] private SceneContext _sceneContext;
        
        //Can be used for more control over scene initialization process, heavy loadings etc.
        //Loading screen hides only after this method is completed.
        public UniTask OnSceneOpen(IProgress<float> progress)
        {
            _sceneContext.ParentContractNames = new[] { nameof(ApplicationInitializer) };
            _sceneContext.Run();
            _sceneContext.Container.Resolve<GameplayLoopStateManager>().SwitchToState<PreparationPhaseState>();
            progress.Report(1f);
            return UniTask.CompletedTask;
        }
    }
}