#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using Tools.SceneManagement.Runtime;
using UnityEditor;
using UnityEngine;

namespace Tools.SceneManagement.Editor
{
    internal static class SceneReferenceEditorUtils
    {
        private const string MENU_ITEM_PATH = "Assets/Create/Create Scene Reference";
        
        [MenuItem(MENU_ITEM_PATH, false, 9)]
        private static void CreateFromSceneObjects()
        {
            foreach (var file in Selection.objects)
            {
                if (file is SceneAsset asset)
                {
                    CreateSceneData(asset);
                }
            }
        }
        
        [MenuItem(MENU_ITEM_PATH, true)]
        private static bool IsSceneAssetSelected()
        {
            foreach (var file in Selection.objects)
            {
                if (file is SceneAsset)
                {
                    return true;
                }
            }

            return false;
        }
        
        internal static IEnumerable<SceneReference> GetSceneReferences()
        {
            return AssetDatabase.FindAssets($"t:{nameof(SceneReference)}")
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<SceneReference>);
        }
        
        internal static IEnumerable<SceneAsset> GetSceneAssets(string ignorePath = null)
        {
            var assets = AssetDatabase.FindAssets($"t:{nameof(SceneAsset)}", new []{"Assets"})
                .Select(AssetDatabase.GUIDToAssetPath);

            if (!string.IsNullOrEmpty(ignorePath))
            {
                assets = assets.Where(path => !path.Contains(ignorePath));
            }

            return assets.Select(AssetDatabase.LoadAssetAtPath<SceneAsset>);
        }
        
        internal static void CreateSceneData(SceneAsset scene)
        {
            var assetObject = ScriptableObject.CreateInstance<SceneReference>();
            assetObject.SceneAsset = scene;
            
            string scenePath = AssetDatabase.GetAssetPath(scene);
            string folderPath = scenePath.Substring(0, scenePath.LastIndexOf('/'));
            string assetPath = $"{folderPath}/{scene.name}_Reference.asset";

            AssetDatabase.CreateAsset(assetObject, assetPath);
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();

            Selection.activeObject = assetObject;
        }
    }
}
#endif