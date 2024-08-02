using Game.Scripts.FieldSystem;
using Game.Scripts.PlayerSystem;
using Game.Scripts.StorageSystem;
using Game.Scripts.TimerSystem;
using UnityEngine;
using Zenject;

namespace Game.Scripts
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField] private Database _database;
        
        public override void InstallBindings()
        {
            //Services initialization order depends on binding order
            
            
            //Container.BindInterfacesTo<LocalJsonStorageService>().AsSingle(); // Encoded Json -> file
            //Container.BindInterfacesTo<BinaryStorageService>().AsSingle(); // Binary -> file
            Container.BindInterfacesTo<PrefsStorageService>().AsSingle(); // Json -> PlayerPrefs
            
            
            Container.BindInterfacesTo<PlayerService>().AsSingle();
            Container.BindInterfacesTo<TimerService>().AsSingle();
            Container.BindInterfacesTo<LevelsService>().AsSingle();
            
            Container.BindInstance(_database.LevelsCollection).WhenInjectedInto<LevelsService>();
        }
    }
}