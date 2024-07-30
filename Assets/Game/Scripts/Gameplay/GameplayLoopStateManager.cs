using System.Collections.Generic;
using Zenject;

namespace Game.Scripts.Gameplay
{
    public class GameplayLoopStateManager : StateManager<GameState>
    {
        [Inject]
        public GameplayLoopStateManager(IEnumerable<State> states) : base(states)
        {
        }
    }
}