using Cysharp.Threading.Tasks;
using Tools.SceneManagement.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Scripts
{
    public class Startup : MonoBehaviour
    {
        [SerializeField] private SceneReferences _sceneRefs;

        private static Startup _instance;
        
        private void Awake()
        {
            _instance = this;
            
            _sceneRefs.Bootstrap
                .LoadScene()
                .WithLoadingScreen(_sceneRefs.Loading)
                .WithMode(LoadSceneMode.Additive)
                .Execute()
                .Forget();
        }
        
        public static void ReloadGame()
        {
            _instance._sceneRefs.Startup
                .LoadScene()
                .WithMode(LoadSceneMode.Single)
                .Execute()
                .Forget();
        }
    }
}
