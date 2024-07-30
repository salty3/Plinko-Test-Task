using UnityEditor;
using UnityEngine;

namespace Tools.Editor.UGUI
{
    public static class UGUITools
    {
        [MenuItem("UGUI/Anchors to Corners %[")]
        public static void AnchorsToCorners()
        {
            foreach (var transform in Selection.transforms)
            {
                var t = transform as RectTransform;
                var pt = Selection.activeTransform.parent as RectTransform;

                if (t == null || pt == null) return;

                var rect = pt.rect;
                var newAnchorsMin = new Vector2(t.anchorMin.x + t.offsetMin.x / rect.width,
                                                    t.anchorMin.y + t.offsetMin.y / rect.height);
                var newAnchorsMax = new Vector2(t.anchorMax.x + t.offsetMax.x / rect.width,
                                                    t.anchorMax.y + t.offsetMax.y / rect.height);

                t.anchorMin = newAnchorsMin;
                t.anchorMax = newAnchorsMax;
                t.offsetMin = t.offsetMax = new Vector2(0, 0);
                EditorUtility.SetDirty(transform);
            }
        }

        [MenuItem("UGUI/Corners to Anchors %]")]
        private static void CornersToAnchors()
        {
            foreach (var transform in Selection.transforms)
            {
                var t = transform as RectTransform;

                if (t == null) return;

                t.offsetMin = t.offsetMax = new Vector2(0, 0);
            }
        }

        [MenuItem("UGUI/Mirror Horizontally Around Anchors %;")]
        private static void MirrorHorizontallyAnchors()
        {
            MirrorHorizontally(false);
        }

        [MenuItem("UGUI/Mirror Horizontally Around Parent Center %:")]
        private static void MirrorHorizontallyParent()
        {
            MirrorHorizontally(true);
        }

        private static void MirrorHorizontally(bool mirrorAnchors)
        {
            foreach (var transform in Selection.transforms)
            {
                var t = transform as RectTransform;
                var pt = Selection.activeTransform.parent as RectTransform;

                if (t == null || pt == null) return;

                if (mirrorAnchors)
                {
                    var oldAnchorMin = t.anchorMin;
                    t.anchorMin = new Vector2(1 - t.anchorMax.x, t.anchorMin.y);
                    t.anchorMax = new Vector2(1 - oldAnchorMin.x, t.anchorMax.y);
                }

                var oldOffsetMin = t.offsetMin;
                t.offsetMin = new Vector2(-t.offsetMax.x, t.offsetMin.y);
                t.offsetMax = new Vector2(-oldOffsetMin.x, t.offsetMax.y);

                var localScale = t.localScale;
                localScale = new Vector3(-localScale.x, localScale.y, localScale.z);
                t.localScale = localScale;
            }
        }

        [MenuItem("UGUI/Mirror Vertically Around Anchors %'")]
        private static void MirrorVerticallyAnchors()
        {
            MirrorVertically(false);
        }

        [MenuItem("UGUI/Mirror Vertically Around Parent Center %\"")]
        private static void MirrorVerticallyParent()
        {
            MirrorVertically(true);
        }

        private static void MirrorVertically(bool mirrorAnchors)
        {
            foreach (var transform in Selection.transforms)
            {
                var t = transform as RectTransform;
                var pt = Selection.activeTransform.parent as RectTransform;

                if (t == null || pt == null) return;

                if (mirrorAnchors)
                {
                    var anchorMin = t.anchorMin;
                    var oldAnchorMin = anchorMin;
                    t.anchorMin = new Vector2(anchorMin.x, 1 - t.anchorMax.y);
                    t.anchorMax = new Vector2(t.anchorMax.x, 1 - oldAnchorMin.y);
                }

                var offsetMin = t.offsetMin;
                var oldOffsetMin = offsetMin;
                t.offsetMin = new Vector2(offsetMin.x, -t.offsetMax.y);
                t.offsetMax = new Vector2(t.offsetMax.x, -oldOffsetMin.y);

                var localScale = t.localScale;
                localScale = new Vector3(localScale.x, -localScale.y, localScale.z);
                t.localScale = localScale;
            }
        }
    }
}