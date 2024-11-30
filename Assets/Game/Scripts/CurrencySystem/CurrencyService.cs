using UnityEngine;

namespace Game.Scripts.CurrencySystem
{
    public class CurrencyService
    {
        private decimal _usdBalance;
        
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