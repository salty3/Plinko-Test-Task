using Cysharp.Threading.Tasks;

namespace Game.Scripts.CurrencySystem
{
    public interface ICurrencyService
    {
        AsyncReactiveProperty<decimal> UsdBalance { get; }
        void AddUsd(decimal amount);
        void SubtractUsd(decimal amount);
    }
}