
using Game.Scripts.BetSystem;
using Game.Scripts.CurrencySystem;
using UnityEngine;
using Zenject;

namespace Game.Scripts.ApplicationCore.ApplicationInitialization
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField] private Database _database;
        
        public override void InstallBindings()
        {
            //Services initialization order depends on binding order
   
            Container.BindInstance(_database.WinValuesConfig).AsSingle();

            Container.BindInterfacesTo<FakeCurrencyService>().AsSingle();
            Container.BindInterfacesTo<BetService>().AsSingle();
        }
    }
}