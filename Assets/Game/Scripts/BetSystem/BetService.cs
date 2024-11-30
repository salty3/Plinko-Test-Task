using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Game.Scripts.BetSystem
{
    public interface IBetService
    {
        AsyncReactiveProperty<decimal> BetAmount { get; }
        
        decimal GetMultiplier(BetRisk risk, int index);
    }

    public enum BetRisk
    {
        Low,
        Medium,
        High
    }
    
    public class BetService : IBetService
    {
        public AsyncReactiveProperty<decimal> BetAmount { get; } = new AsyncReactiveProperty<decimal>(100m);
        
        private WinValuesConfig _winValuesConfig;

        
        [Inject]
        public BetService(WinValuesConfig winValuesConfig)
        {
            _winValuesConfig = winValuesConfig;
        }
        
        public decimal GetMultiplier(BetRisk risk, int index)
        {
            if (index != 0)
            {
                index /= 2;
            }
            return risk switch
            {
                BetRisk.Low => Convert.ToDecimal(_winValuesConfig.GetLowBetMultiplier(index)),
                BetRisk.Medium => Convert.ToDecimal(_winValuesConfig.GetMediumBetMultiplier(index)),
                BetRisk.High => Convert.ToDecimal(_winValuesConfig.GetHighBetMultiplier(index)),
                _ => throw new ArgumentOutOfRangeException(nameof(risk), risk, null)
            };
        }
    }
}