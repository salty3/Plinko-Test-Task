using System.Linq;

namespace Tools.SceneManagement.Runtime
{
    public static class SceneAssetExtensions
    {
        public static SceneLoadingBuilder LoadScene(this SceneReference sceneReference)
        {
            return new SceneLoadingBuilder(sceneReference);
        }
        
        internal static T GetRootObject<T> (this SceneReference sceneReference)
        {
            var targetObject = sceneReference.Scene
                .GetRootGameObjects()
                .FirstOrDefault(go => go.TryGetComponent<T>(out _));
            
            return targetObject != null ? targetObject.GetComponent<T>() : default;
        }
        
        internal static T GetAnyObject<T> (this SceneReference sceneReference)
        {
            return sceneReference.Scene
                .GetRootGameObjects()
                .SelectMany(go => go.GetComponentsInChildren<T>())
                .FirstOrDefault();
        }
    }
}