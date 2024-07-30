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
            Container.Bind<GameplayLoopStateManager>().AsSingle();
            //States
            Container.Bind<PlayerInteractionState>().AsSingle();
            Container.Bind<ShuffleCardsState>().AsSingle();
            Container.Bind<PreparationPhaseState>().AsSingle();
        }
    }
}