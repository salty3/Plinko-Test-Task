using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Game.Scripts.BetSystem
{
    public class BetService : IBetService
    {
        public AsyncReactiveProperty<decimal> BetAmount { get; }
        
        private readonly WinValuesConfig _winValuesConfig;
        private readonly decimal[] _betPresets;

        private int _currentPresetIndex;

        
        [Inject]
        public BetService(WinValuesConfig winValuesConfig, BetsConfig betsConfig)
        {
            _winValuesConfig = winValuesConfig;
            _betPresets = betsConfig.BetPresets.Select(Convert.ToDecimal).ToArray();
            _currentPresetIndex = 0;
            BetAmount = new AsyncReactiveProperty<decimal>(_betPresets[_currentPresetIndex]);
        }
        
        public decimal GetMultiplier(BetRisk risk, int index)
        {
            if (index != 0)
            {
                index /= 2;
            }
            return Convert.ToDecimal(_winValuesConfig.GetMultiplier(risk, index));
        }

        public void IncrementBet()
        {
            if (_currentPresetIndex < _betPresets.Length - 1)
            {
                _currentPresetIndex++;
                BetAmount.Value = _betPresets[_currentPresetIndex];
            }
        }

        public void DecrementBet()
        {
            if (_currentPresetIndex > 0)
            {
                _currentPresetIndex--;
                BetAmount.Value = _betPresets[_currentPresetIndex];
            }
        }
    }
}