using UnityEditor;
using UnityEditor.Overlays;
using UnityEditor.Toolbars;
using UnityEngine;

namespace Tools.Editor.UGUI
{
    [Overlay(typeof(SceneView), "")]
    public class UGUIHelperOverlay : ToolbarOverlay
    {
        private UGUIHelperOverlay() : base(AnchorsToCorners.ID)
        {
            
        }
    }
    
    [EditorToolbarElement(ID, typeof(SceneView))]
    public class AnchorsToCorners : EditorToolbarButton
    {
        public const string ID = "UGUIHelper/Anchors To Corners";
    
        public AnchorsToCorners()
        {
            text = "";
            icon = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Tools/Editor/UGUI/anchors-to-corners.png");
            tooltip = "Snap Anchors To Corners for all selected RectTransforms";
            clicked += OnClick;
        }

        private static void OnClick()
        {
           UGUITools.AnchorsToCorners();
        }
    }
}