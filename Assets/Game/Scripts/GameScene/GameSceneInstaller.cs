using Game.Scripts.Gameplay;
using Zenject;

namespace Game.Scripts
{
    public class GameSceneInstaller : MonoInstaller
    {
        
        public override void InstallBindings()
        {
            Container.Bind<CardsFieldView>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<CardsFieldPresenter>().AsSingle();
            
            Container.Bind<GameplayLoopStateManager>().FromSubContainerResolve().ByInstaller<GameStatesInstaller>().AsSingle();
        }
    }
}