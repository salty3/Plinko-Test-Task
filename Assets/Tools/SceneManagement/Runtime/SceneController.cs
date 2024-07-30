using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Tools.Runtime;
using UnityEngine.SceneManagement;
using Zenject;

namespace Tools.SceneManagement.Runtime
{
    public class SceneController : SingletonBehaviour<SceneController>
    {
        private ZenjectSceneLoader _sceneLoader;
        private readonly Dictionary<string, ILoadingScreen> _openedLoadingScreens = new();
        
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

        internal async UniTask LoadAsync(SceneLoadingBuilder sceneLoadingBuilder)
        {
            var loadingScreen = await OpenLoadingScreen(sceneLoadingBuilder);
            var progress = GetProgress(loadingScreen);
            await LoadScene(sceneLoadingBuilder, progress);
            await NotifyEntryPoint(sceneLoadingBuilder, progress);

            if (loadingScreen != null)
            {
                await loadingScreen.Close();
            }
        }
    }
}