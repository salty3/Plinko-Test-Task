using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Scripts.Gameplay;
using Game.Scripts.GameScene.Gameplay;
using Game.Scripts.PlayerSystem;
using Zenject;

namespace Game.Scripts.FieldSystem
{
    public class LevelsService : ILevelsService
    {
        private readonly IPlayerService _playerService;
        private readonly LevelsCollection _levelsCollection;
        private readonly List<LevelEntity> _levelEntities;

        public IEnumerable<IReadOnlyLevelEntity> Levels => _levelEntities;
        

        [Inject]
        public LevelsService(IPlayerService playerService, LevelsCollection levelsCollection)
        {
            _playerService = playerService;
            _levelsCollection = levelsCollection;
            _levelEntities = new List<LevelEntity>();
        }

        public UniTask Initialize(CancellationToken token)
        {
            var levelsSaveData = _playerService.GetData(data => data.LevelsSaveData);
            
            for (var index = 0; index < _levelsCollection.Levels.Count(); index++)
            {
                //Not protected from level data changes. But it's not necessary now
                var saveData = levelsSaveData.ElementAtOrDefault(index);
                if (saveData == null)
                {
                    saveData = new LevelEntity.SaveData()
                    {
                        LevelIndex = index
                    };
                    levelsSaveData.Add(saveData);
                }
                
                _levelEntities.Add(new LevelEntity(saveData));
            }
            
            return default;
        }
        
        public void CreateField(int levelIndex, out bool newField)
        {
            var levelEntity = _levelEntities[levelIndex];
            var levelData = GetLevelData(levelIndex);
            levelEntity.CreateField(levelData, out newField);
        }

        public void SetAsCompleted(int levelIndex, int score)
        {
            var levelEntity = _levelEntities[levelIndex];
            levelEntity.SetAsCompleted(score);
        }

        public void CompletePair(int levelIndex, string id)
        {
            var levelEntity = _levelEntities[levelIndex];
            levelEntity.CompleteFieldPair(id);
        }

        public void ShuffleField(int levelIndex, int seed)
        {
            var levelEntity = _levelEntities[levelIndex];
            levelEntity.ShuffleField(seed);
        }

        public void AddMatch(int levelIndex)
        {
            var levelEntity = _levelEntities[levelIndex];
            levelEntity.AddMatch();
        }
        
        public void AddMismatch(int levelIndex)
        {
            var levelEntity = _levelEntities[levelIndex];
            levelEntity.AddMismatch();
        }
        
        public void ResetLevel(int levelIndex)
        {
            var levelEntity = _levelEntities[levelIndex];
            levelEntity.Reset();
        }
        
        public void UpdateElapsedTime(int levelIndex, TimeSpan time)
        {
            var levelEntity = _levelEntities[levelIndex];
            levelEntity.ElapsedTime = time;
        }

        public LevelData GetLevelData(int levelIndex)
        {
            return _levelsCollection.Levels.ElementAtOrDefault(levelIndex);
        }
    }
}