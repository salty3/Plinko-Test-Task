using System;
using UnityEngine;

namespace Game.Scripts.LevelsSystem.Card
{
    public interface IReadOnlyCardEntity
    {
        string ID { get; }
        bool IsMatched { get; }
    }

    public class CardEntity : IReadOnlyCardEntity
    {
        private readonly SaveData _data;

        public string ID 
        { 
            get => _data.ID;
            private set => _data.ID = value;
        }
        
        public bool IsMatched 
        {
            get => _data.IsMatched;
            private set => _data.IsMatched = value;
        }
        
        public CardEntity(SaveData data)
        {
            _data = data;
        }
        
        public void SetMatched()
        {
            IsMatched = true;
        }
        
        [Serializable]
        public class SaveData
        {
            [field: SerializeField] public string ID { get; set; }
            [field: SerializeField] public bool IsMatched { get; set; }
        }
    }
}