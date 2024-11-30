using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Scripts.CurrencySystem
{
    public class FakeCurrencyService : ICurrencyService
    {
        private decimal _usdBalance;

        public AsyncReactiveProperty<decimal> UsdBalance { get; }

        public void AddUsd(decimal amount)
        {
            _usdBalance += amount;
        }
        
        public void SubtractUsd(decimal amount)
        {
            _usdBalance -= amount;
        }
    }
}