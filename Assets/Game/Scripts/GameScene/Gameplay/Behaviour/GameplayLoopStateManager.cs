using System;
using System.Collections.Generic;
using Game.Scripts.Gameplay.States;
using Tools.Runtime.StateBehaviour;
using Zenject;

namespace Game.Scripts.Gameplay
{
    public class GameplayLoopStateManager : StateManager<GameState>
    {
        [Inject]
        public GameplayLoopStateManager(DiContainer container) : base(new []
        {
            typeof(PlayerInteractionState),
            typeof(ShuffleCardsState),
            typeof(PreparationPhaseState)
        }, container)
        {
        }
    }
}