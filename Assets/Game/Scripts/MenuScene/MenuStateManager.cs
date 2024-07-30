using System.Collections.Generic;
using Game.Scripts.Gameplay;

namespace Game.Scripts.MenuScene
{
    public class MenuStateManager : StateManager<MenuState>
    {
        public MenuStateManager(IEnumerable<State> states) : base(states)
        {
        }
    }
}