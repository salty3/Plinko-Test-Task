using System;
using Cysharp.Threading.Tasks;
using Game.Scripts.FieldSystem;
using Game.Scripts.GameScene.Gameplay.Behaviour;
using Game.Scripts.GameScene.Gameplay.Behaviour.States;
using Game.Scripts.TimerSystem;
using Tools.Runtime;
using Tools.SceneManagement.Runtime;
using UnityEngine;
using Zenject;

namespace Game.Scripts.GameScene.GameSceneEntry
{
    public class GameSceneEntryPoint : CachedMonoBehaviour, ISceneEntryPoint
    {
        [SerializeField] private SceneContext _sceneContext;

        private Timer _timer;
        private ILevelsService _levelsService;
        private IReadOnlyLevelEntity _levelEntity;
        
        [Inject]
        private void Construct(Timer timer, ILevelsService levelsService, IReadOnlyLevelEntity levelEntity)
        {
            _timer = timer;
            _levelsService = levelsService;
            _levelEntity = levelEntity;
        }
        
        //Can be used for more control over scene initialization process, heavy loadings etc.
        //Loading screen hides only after this method is completed.
        public UniTask OnSceneOpen(IProgress<float> progress)
        {
            _sceneContext.ParentContractNames = new[] { nameof(ApplicationInitializer) };
            _sceneContext.Run();
            _sceneContext.Container.Resolve<GameplayLoopStateManager>().SwitchToState<PreparationPhaseState>();

            _timer.Updated += UpdateTimer;
            
            progress.Report(1f);
            return UniTask.CompletedTask;
        }

        private void UpdateTimer()
        {
            _levelsService.UpdateElapsedTime(_levelEntity.LevelIndex, _timer.ElapsedTime);
        }
        
        private void OnDestroy()
        {
            _timer.Dispose();
        }
    }
}