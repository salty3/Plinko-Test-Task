using Cysharp.Threading.Tasks;
using Tools.SceneManagement.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game.Scripts
{
    //Game entry point
    public class Startup : MonoBehaviour
    {
        [SerializeField] private SceneContext _projectContext;

        private static Startup _instance;

        private SceneController _sceneController;
        private SceneReferences _sceneRefs;
        
        private void Awake()
        {
            _instance = this;
            //Kinda awkward VContainer-like parenting flow :)
            _projectContext.ContractNames = new[] { nameof(Startup) };
            _projectContext.Run();
            _sceneController = _projectContext.Container.Resolve<SceneController>();
            _sceneRefs = _projectContext.Container.Resolve<SceneReferences>();

            var builder = _sceneRefs.Bootstrap
                .LoadScene()
                .WithLoadingScreen(_sceneRefs.Loading)
                .WithMode(LoadSceneMode.Additive);

            _sceneController.LoadAsync(builder).Forget();
        }
        
        public static void ReloadGame()
        {
            var builder = _instance._sceneRefs.Startup
                .LoadScene()
                .WithMode(LoadSceneMode.Single);
            
            _instance._sceneController.LoadAsync(builder).Forget();
        }
    }
}
