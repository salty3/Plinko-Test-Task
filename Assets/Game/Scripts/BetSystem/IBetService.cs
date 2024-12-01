using Cysharp.Threading.Tasks;

namespace Game.Scripts.BetSystem
{
    public interface IBetService
    {
        AsyncReactiveProperty<decimal> BetAmount { get; }
        
        decimal GetMultiplier(BetRisk risk, int index);

        void IncrementBet();
        void DecrementBet();
    }
}