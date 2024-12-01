using Cysharp.Threading.Tasks;

namespace Game.Scripts.CurrencySystem
{
    public class FakeCurrencyService : ICurrencyService
    {
        public AsyncReactiveProperty<decimal> UsdBalance { get; } = new(500m);
        
        public void AddUsd(decimal amount)
        {
            UsdBalance.Value += amount;
        }

        public bool TrySubtractUsd(decimal amount)
        {
            if (UsdBalance.Value < amount)
            {
                return false;
            }

            UsdBalance.Value -= amount;
            return true;
        }
    }
}