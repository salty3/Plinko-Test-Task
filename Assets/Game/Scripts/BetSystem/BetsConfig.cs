using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.BetSystem
{
    [CreateAssetMenu(menuName = "Game/BetSystem/BetsConfig")]
    public class BetsConfig : ScriptableObject
    {
        [SerializeField] private float[] _betPresets;
        
        public IEnumerable<float> BetPresets => _betPresets;
    }
}