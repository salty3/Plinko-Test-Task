using System;
using UnityEngine;

namespace Game.Scripts.Gameplay.Card
{
    [CreateAssetMenu(fileName = "CardData", menuName = "Game/CardData")]
    public class CardData : ScriptableObject
    {
        //Should use meaningful IDs (to link with excel sheets for example).
        [field: SerializeField] public string ID { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
        
    }
}