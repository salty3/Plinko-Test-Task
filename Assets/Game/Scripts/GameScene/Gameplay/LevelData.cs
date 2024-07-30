using System;
using System.Collections.Generic;
using Game.Scripts.Gameplay.Card;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Scripts.Gameplay
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Game/LevelData")]
    public class LevelData : ScriptableObject
    {
        private const int MAX_PAIRS = 12;
        
        [field: SerializeField] public Sprite LevelPreviewIcon { get; set; }
        [field: SerializeField] public Sprite CardBack { get; set; }
        [field: SerializeField] public Sprite LevelBackground { get; set; }
        
        //Here should be some validation for max amount of pairs or level layering logic
        [FormerlySerializedAs("_cardPairs")] [SerializeField] private CardData[] _cards = new CardData[MAX_PAIRS];
        
        public IEnumerable<CardData> Cards => _cards;
    }
}