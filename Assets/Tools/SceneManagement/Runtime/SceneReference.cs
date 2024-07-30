using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tools.SceneManagement.Runtime
{
    public class SceneReference : ScriptableObject
#if UNITY_EDITOR
        , ISerializationCallbackReceiver
#endif
    {
        
        [SerializeField, HideInInspector] private string _sceneName;
        [SerializeField, HideInInspector] private int _sceneIndex;
        [SerializeField, HideInInspector] private string _scenePath;
        
        internal string SceneName { get => _sceneName; set => _sceneName = value; }
        internal int SceneIndex { get => _sceneIndex; set => _sceneIndex = value; }
        internal string ScenePath { get => _scenePath; set => _scenePath = value; }
        
        internal Scene Scene => SceneManager.GetSceneByName(_sceneName);

        #region EDITOR

#if UNITY_EDITOR
        [SerializeField] private SceneAsset _scene;
        
        internal SceneAsset SceneAsset { get => _scene; set => _scene = value; }
        
        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {

            string nameValue = _scene != null ? _scene.name : null;
            int indexValue = -2;
            string pathValue = _scene != null ? AssetDatabase.GetAssetPath(_scene) : null;

            var buildSettingsScenes = EditorBuildSettings.scenes;
            if (buildSettingsScenes.Length > 0)
            {
                for (var i = 0; i < buildSettingsScenes.Length; i++)
                {
                    if (EditorBuildSettings.scenes[i].path == pathValue)
                    {
                        indexValue = i;
                        break;
                    }
                }
            }

            _sceneName = nameValue;
            _sceneIndex = indexValue;
            _scenePath = pathValue;

        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
        }

        internal bool IsInBuildSettings()
        {
            if (_scene == null)
            {
                return false;
            }

            var scenes = EditorBuildSettings.scenes;
            return scenes.Any(s => s.path == _scenePath);
        }
        
        private const string GROUP_NAME = "Horizontal Group";
        
        //[Button(ButtonStyle.Box), HorizontalGroup(GROUP_NAME)]
        internal void OpenSingle()
        {
            EditorSceneManager.OpenScene(ScenePath, OpenSceneMode.Single);
        }
        
        //[Button(ButtonStyle.Box), HorizontalGroup(GROUP_NAME)]
        internal void OpenAdditive()
        {
            EditorSceneManager.OpenScene(ScenePath, OpenSceneMode.Additive);
        }

        //[Button, HideIf(nameof(IsInBuildSettings))]
        internal void AddToBuildSettings()
        {
            if (_scene == null)
            {
                return;
            }

            var scenes = EditorBuildSettings.scenes;

            var newScenes = new EditorBuildSettingsScene[scenes.Length + 1];
            scenes.CopyTo(newScenes, 0);
            newScenes[^1] = new EditorBuildSettingsScene(_scenePath, true);
            EditorBuildSettings.scenes = newScenes;
        }

        //[Button, ShowIf(nameof(IsInBuildSettings))]
        internal void RemoveFromBuildSettings()
        {
            if (_scene == null)
            {
                return;
            }

            var scenes = EditorBuildSettings.scenes;

            var newScenes = scenes.Where(s => s.path != _scenePath).ToArray();
            EditorBuildSettings.scenes = newScenes;
        }
  #endif

        #endregion
    }
}