using Game.Scripts.Gameplay;
using Game.Scripts.TimerSystem;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Game.Scripts
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField] private LevelData _levelData;
        
        public override void InstallBindings()
        {
            ServicesInstaller.Install(Container);

            Container.BindInstance(_levelData);
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