using Cysharp.Threading.Tasks;

namespace Game.Scripts.BetSystem
{
    public interface IBetService
    {
        AsyncReactiveProperty<decimal> BetAmount { get; }
    }
    
    public class BetService : IBetService
    {
        public AsyncReactiveProperty<decimal> BetAmount { get; }
    }
}