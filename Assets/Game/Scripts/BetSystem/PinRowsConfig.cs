using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.BetSystem
{
    [CreateAssetMenu(menuName = "Game/BetSystem/PinRowsConfig")]
    public class PinRowsConfig : ScriptableObject
    {
        [SerializeField] private int[] _pinRowsAmount;
        
        public IEnumerable<int> PinRowsAmount => _pinRowsAmount;
    }
}