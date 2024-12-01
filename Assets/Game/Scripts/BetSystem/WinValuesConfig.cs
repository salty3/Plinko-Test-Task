using UnityEngine;

namespace Game.Scripts.BetSystem
{
    [CreateAssetMenu(menuName = "Game/BetSystem/WinValuesConfig")]
    public class WinValuesConfig : ScriptableObject
    {
        [SerializeField] private float[] _lowBetMultipliers;
        [SerializeField] private float[] _mediumBetMultipliers;
        [SerializeField] private float[] _highBetMultipliers;
        
        public float GetMultiplier(BetRisk risk, int index)
        {
            return risk switch
            {
                BetRisk.Low => _lowBetMultipliers[Mathf.Abs(index)],
                BetRisk.Medium => _mediumBetMultipliers[Mathf.Abs(index)],
                BetRisk.High => _highBetMultipliers[Mathf.Abs(index)],
                _ => throw new System.ArgumentOutOfRangeException(nameof(risk), risk, null)
            };
        }
    }
}