using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using Zenject;

namespace Tools.SceneManagement.Runtime
{
    public class SceneController
    {
        private readonly ZenjectSceneLoader _sceneLoader;
        private readonly Dictionary<string, ILoadingScreen> _openedLoadingScreens = new();
        
        [Inject]
        public SceneController(ZenjectSceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }
        
        private async UniTask<ILoadingScreen> OpenLoadingScreen(SceneLoadingBuilder sceneLoadingBuilder)
        {
            var loadingScreenReference = sceneLoadingBuilder.LoadingScreenReference;
            
            if (sceneLoadingBuilder.LoadingScreenReference == null)
            {
                return null;
            }

            if (!_openedLoadingScreens.TryGetValue(loadingScreenReference.SceneName, out var loadingScreen))
            {
                await _sceneLoader.LoadSceneAsync(loadingScreenReference.SceneName, LoadSceneMode.Additive);
                loadingScreen = loadingScreenReference.GetRootObject<ILoadingScreen>();
                _openedLoadingScreens.Add(loadingScreenReference.SceneName, loadingScreen);
            }

            await loadingScreen.Open();
            return loadingScreen;
        }

        private async UniTask LoadScene(SceneLoadingBuilder sceneLoadingBuilder, IProgress<float> progress)
        {
            var operation = _sceneLoader.LoadSceneAsync(sceneLoadingBuilder.SceneReference.SceneName,
                sceneLoadingBuilder.LoadSceneMode,
                sceneLoadingBuilder.AdditionalRegistrations);

            operation.allowSceneActivation = false;
            while (operation.progress < 0.9f)
            {
                await UniTask.Yield();
                progress.Report(operation.progress / 2f);
            }
            operation.allowSceneActivation = true;

            await operation;
        }
        
        private async UniTask NotifyEntryPoint(SceneLoadingBuilder sceneLoadingBuilder, IProgress<float> progress)
        {
            var entryPoint = sceneLoadingBuilder.SceneReference.GetRootObject<ISceneEntryPoint>();

            if (entryPoint != null)
            {
                var sceneOpenProgress = Progress.Create<float>(p => progress.Report(0.5f + p / 2f));
                await entryPoint.OnSceneOpen(sceneOpenProgress);
            }
            else
            {
                progress.Report(1f);
            }
        }
        
        private IProgress<float> GetProgress(ILoadingScreen loadingScreen)
        {
            return loadingScreen != null
                ? Progress.Create<float>(loadingScreen.SetProgress)
                : Progress.Create<float>(_ => { });
        }

        public async UniTask LoadAsync(SceneLoadingBuilder sceneLoadingBuilder)
        {
            var loadingScreen = await OpenLoadingScreen(sceneLoadingBuilder);
            var progress = GetProgress(loadingScreen);
            if (sceneLoadingBuilder.SceneToClose != null)
            {
                await SceneManager.UnloadSceneAsync(sceneLoadingBuilder.SceneToClose.SceneName);
            }
            await LoadScene(sceneLoadingBuilder, progress);
            await NotifyEntryPoint(sceneLoadingBuilder, progress);

            if (loadingScreen != null)
            {
                await loadingScreen.Close();
            }
        }
    }
}