using System;
using System.Collections.Generic;
using Game.Scripts.ApplicationCore;
using Game.Scripts.LevelsSystem.Levels;

namespace Game.Scripts.LevelsSystem
{
    public interface ILevelsService : IService
    {
        void CreateField(int levelIndex, out bool newField);
        void SetAsCompleted(int levelIndex);
        void CompletePair(int levelIndex, string id);
        void ShuffleField(int levelIndex, int seed);
        void AddMatch(int levelIndex);
        void AddMismatch(int levelIndex);
        void ResetLevel(int levelIndex);
        void UpdateElapsedTime(int levelIndex, TimeSpan time);
        int GetUnlockedLevel();
        
        IEnumerable<IReadOnlyLevelEntity> Levels { get; }
        LevelData GetLevelData(int levelIndex);
    }
}