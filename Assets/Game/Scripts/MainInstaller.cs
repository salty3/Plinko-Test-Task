using Game.Scripts.Gameplay;
using Game.Scripts.TimerSystem;
using JetBrains.Annotations;
using Zenject;

namespace Game.Scripts
{
    public class MainInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            ServicesInstaller.Install(Container);
            
        }
    }
    
    [UsedImplicitly]
    public class ServicesInstaller : Installer<ServicesInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<TimerService>().AsSingle();
        }
    }
}