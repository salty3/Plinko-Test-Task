
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
   
        }
    }
}