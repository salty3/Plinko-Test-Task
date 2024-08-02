using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using Zenject;

namespace Tools.SceneManagement.Runtime
{
    public class SceneLoadingBuilder
    {
        internal readonly SceneReference SceneReference;
        internal SceneReference LoadingScreenReference;
        internal SceneReference SceneToClose;
        internal LoadSceneMode LoadSceneMode = LoadSceneMode.Additive;
        internal Action<DiContainer> AdditionalRegistrations;
        

        internal SceneLoadingBuilder(SceneReference sceneReference)
        {
            SceneReference = sceneReference;
        }
        
        public SceneLoadingBuilder WithMode(LoadSceneMode loadSceneMode)
        {
            LoadSceneMode = loadSceneMode;
            return this;
        }
        
        public SceneLoadingBuilder WithLoadingScreen(SceneReference loadingScreenReference)
        {
            LoadingScreenReference = loadingScreenReference;
            return this;
        }
        
        public SceneLoadingBuilder WithClosing(SceneReference sceneReference)
        {
            SceneToClose = sceneReference;
            return this;
        }
        
        public SceneLoadingBuilder WithRegistrations(Action<DiContainer> additionalRegistrations)
        {
            AdditionalRegistrations = additionalRegistrations;
            return this;
        }
    }
}