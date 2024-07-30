using Game.Scripts.TimerSystem;
using JetBrains.Annotations;
using Zenject;

namespace Game.Scripts
{
    [UsedImplicitly]
    public class ServicesInstaller : Installer<ServicesInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<TimerService>().AsSingle();
        }
    }
}