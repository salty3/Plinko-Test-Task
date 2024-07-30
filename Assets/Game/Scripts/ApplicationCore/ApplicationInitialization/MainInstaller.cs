using Game.Scripts.Gameplay;
using UnityEngine;
using Zenject;

namespace Game.Scripts
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField] private Database _database;
        
        public override void InstallBindings()
        {
            ServicesInstaller.Install(Container);

            Container.BindInstance(_database.LevelsCollection);
        }
    }
}