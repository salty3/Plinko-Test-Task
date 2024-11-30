using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Scripts.CurrencySystem
{
    public class FakeCurrencyService : ICurrencyService
    {
        public AsyncReactiveProperty<decimal> UsdBalance { get; } = new AsyncReactiveProperty<decimal>(2000.50m);
        
        public void AddUsd(decimal amount)
        {
            UsdBalance.Value += amount;
        }
        
        public void SubtractUsd(decimal amount)
        {
            UsdBalance.Value -= amount;
        }
    }
}