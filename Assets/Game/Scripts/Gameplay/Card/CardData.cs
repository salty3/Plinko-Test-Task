using System;
using UnityEngine;

namespace Game.Scripts.Gameplay.Card
{
    public class CardData : ScriptableObject
    {
        //Should use meaningful IDs (to link with excel sheets for example). But just GUID for now
        [field: SerializeField, HideInInspector] public string ID { get; private set; } = Guid.NewGuid().ToString();
        [field: SerializeField] public Sprite Icon { get; private set; }
        
    }
}