using System;
using System.Collections.Generic;
using Game.Scripts.Gameplay.Card;
using Game.Scripts.GameScene.Gameplay;
using Game.Scripts.GameScene.Gameplay.Card;
using Tools.Runtime;
using UnityEngine;
using Random = System.Random;

namespace Game.Scripts.Gameplay
{
    public interface IReadOnlyCardsFieldEntity
    {
        IReadOnlyList<IReadOnlyCardEntity> Cards { get; }
    }
    
    public class CardsFieldEntity : IReadOnlyCardsFieldEntity
    {
        private SaveData _saveData;
        private List<CardEntity> _cards = new();
        
        public IReadOnlyList<IReadOnlyCardEntity> Cards => _cards.ConvertAll(c => (IReadOnlyCardEntity) c);


        public CardsFieldEntity(SaveData saveData)
        {
            _saveData = saveData;
            _cards = saveData.Cards.ConvertAll(data => new CardEntity(data));
        }
        
        public CardsFieldEntity(SaveData saveData, LevelData levelData)
        {
            _saveData = saveData;
            foreach (var cardData in levelData.Cards)
            {
                var cardEntity1 = CreateCardEntity(cardData);
                var cardEntity2 = CreateCardEntity(cardData);
                
                _cards.Add(cardEntity1);
                _cards.Add(cardEntity2);
            }
        }

        private CardEntity CreateCardEntity(CardData data)
        {
            var saveData = new CardEntity.SaveData()
            {
                ID = data.ID,
                IsMatched = false
            };
            _saveData.Cards.Add(saveData);
            var entity = new CardEntity(saveData);
            return entity;
        }
        

        public void Shuffle(int seed)
        {
            _cards.Shuffle(seed);
            _saveData.Cards.Shuffle(seed);
        }
        
        public void CompletePair(string id)
        {
            var pair = _cards.FindAll(c => c.ID == id);
            
            foreach (var cardEntity in pair)
            {
                cardEntity.SetMatched();
            }
        }
        
        [Serializable]
        public class SaveData
        {
            [field: SerializeField] public List<CardEntity.SaveData> Cards { get; set; } = new();
        }

       
    }
}