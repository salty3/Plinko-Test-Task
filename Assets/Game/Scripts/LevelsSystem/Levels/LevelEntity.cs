using System;
using Game.Scripts.LevelsSystem.Field;
using UnityEngine;

namespace Game.Scripts.LevelsSystem.Levels
{
    public interface IReadOnlyLevelEntity
    {
        int LevelIndex { get; }
        IReadOnlyCardsFieldEntity Field { get; }
        bool IsCompleted { get; }
        TimeSpan ElapsedTime { get; }
        int MatchesCount { get; }
        int MismatchesCount { get; }
    }
    
    public class LevelEntity : IReadOnlyLevelEntity
    {
        private readonly SaveData _saveData;

        public int LevelIndex => _saveData.LevelIndex;

        public IReadOnlyCardsFieldEntity Field => _field;

        private CardsFieldEntity _field;
        
        public bool IsCompleted
        {
            get => _saveData.IsCompleted;
            private set => _saveData.IsCompleted = value;
        }
        
        public TimeSpan ElapsedTime
        {
            get => new(_saveData.ElapsedTime);
            set => _saveData.ElapsedTime = value.Ticks;
        }

        public int MatchesCount
        {
            get => _saveData.MatchesCount;
            private set => _saveData.MatchesCount = value;
        }

        public int MismatchesCount
        {
            get => _saveData.MismatchesCount;
            private set => _saveData.MismatchesCount = value;
        }

        public LevelEntity(SaveData saveData)
        {
            _saveData = saveData;
            _field = saveData.FieldSaveData != null ? new CardsFieldEntity(saveData.FieldSaveData) : null;
        }
        
        public void SetAsCompleted()
        {
            IsCompleted = true;
            _field = null;
            _saveData.FieldSaveData = null;
        }
        
        public void CreateField(LevelData levelData, out bool newField)
        {
            if (_saveData.FieldSaveData != null && _saveData.FieldSaveData.Cards.Count > 0)
            {
                newField = false;
                //Just use save if exists
                return;
            }
            var saveData = new CardsFieldEntity.SaveData();
            _saveData.FieldSaveData = saveData;
            _field = new CardsFieldEntity(saveData, levelData);
            newField = true;
        }
        
        public void ShuffleField(int seed)
        {
            _field.Shuffle(seed);
        }
        
        public void CompleteFieldPair(string id)
        {
            _field.CompletePair(id);
        }
        
        public void AddMatch()
        {
            MatchesCount++;
        }
        
        public void AddMismatch()
        {
            MismatchesCount++;
        }
        
        public void Reset()
        {
            IsCompleted = false;
            ElapsedTime = TimeSpan.Zero;
            MatchesCount = 0;
            MismatchesCount = 0;
            _field = null;
            _saveData.FieldSaveData = null;
        }
        

        [Serializable]
        public class SaveData
        {
            [field: SerializeField] public int LevelIndex { get; set; }
            [field: SerializeField] public CardsFieldEntity.SaveData FieldSaveData { get; set; }
            [field: SerializeField] public bool IsCompleted { get; set; }
            [field: SerializeField] public long ElapsedTime { get; set; } 
            [field: SerializeField] public int MatchesCount { get; set; }
            [field: SerializeField] public int MismatchesCount { get; set; }
        }
    }
}