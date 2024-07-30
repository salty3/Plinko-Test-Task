using Game.Scripts.Gameplay.States;
using JetBrains.Annotations;
using Zenject;

namespace Game.Scripts.Gameplay
{
    [UsedImplicitly]
    public class GameStatesInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.Bind<GameplayLoopStateManager>();
            //States
            Container.Bind<PlayerInteractionState>();
            Container.Bind<ShuffleCardsState>();
            Container.Bind<PreparationPhaseState>();
        }
    }
}