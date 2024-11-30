using UnityEngine;

namespace Game.Scripts.BetSystem
{
    [CreateAssetMenu(menuName = "Game/BetSystem/WinValuesConfig")]
    public class WinValuesConfig : ScriptableObject
    {
        [SerializeField] private float[] _lowBetMultipliers;
        [SerializeField] private float[] _mediumBetMultipliers;
        [SerializeField] private float[] _highBetMultipliers;
        
        public float GetLowBetMultiplier(int index)
        {
            return _lowBetMultipliers[Mathf.Abs(index)];
        }
        
        public float GetMediumBetMultiplier(int index)
        {
            return _mediumBetMultipliers[Mathf.Abs(index)];
        }
        
        public float GetHighBetMultiplier(int index)
        {
            return _highBetMultipliers[Mathf.Abs(index)];
        }
    }
}