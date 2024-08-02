using Tools.SceneManagement.Runtime;
using UnityEngine;
using Zenject;

namespace Game.Scripts.ApplicationCore.ApplicationEntry
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private SceneReferences _sceneRefs;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_sceneRefs);
            Container.Bind<SceneController>().AsSingle().NonLazy();
        }
    }
}