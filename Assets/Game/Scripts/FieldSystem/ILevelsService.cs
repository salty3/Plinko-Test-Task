using System;
using System.Collections.Generic;
using Game.Scripts.Gameplay;
using Game.Scripts.GameScene.Gameplay;

namespace Game.Scripts.FieldSystem
{
    public interface ILevelsService : IService
    {
        void CreateField(int levelIndex, out bool newField);
        void SetAsCompleted(int levelIndex, int score);
        void CompletePair(int levelIndex, string id);
        void ShuffleField(int levelIndex, int seed);
        void AddMatch(int levelIndex);
        void AddMismatch(int levelIndex);
        void ResetLevel(int levelIndex);
        void UpdateElapsedTime(int levelIndex, TimeSpan time);
        
        IEnumerable<IReadOnlyLevelEntity> Levels { get; }
        LevelData GetLevelData(int levelIndex);
    }
}