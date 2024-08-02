using UnityEngine;

namespace Tools.Runtime.SafeArea
{
    [ExecuteAlways]
    public sealed class UISafeArea : UIMonoBehaviour
    {
        [SerializeField] private bool _ignoreLeft;
        [SerializeField] private bool _ignoreRight;
        [SerializeField] private bool _ignoreTop;
        [SerializeField] private bool _ignoreBottom;
        
        public static bool Ignore { get; set; }
        
        private RectTransform _rectTransformCache;
        private Rect _currentArea;
        private bool _isForce;
        
        protected override void Start()
        {
            Apply();
        }

#if UNITY_EDITOR
        private void Update()
        {
            if (Application.isPlaying)
            {
                return;
            }

            Apply(_isForce);
            _isForce = false;
        }
        
        protected override void OnValidate()
        {
            if (Application.isPlaying)
            {
                return;
            }

            _isForce = true;
        }
#endif
        
        private void Apply(bool isForce = false)
        {
            if (Ignore)
            {
                return;
            }
            ApplyFrom(Screen.safeArea, isForce);
        }
        
        private void ApplyFrom(Rect area, bool isForce = false)
        {
            if (RectTransform == null)
            {
                return;
            }

            if (!isForce && _currentArea == area)
            {
                return;
            }

            var anchorMin = area.position;
            var anchorMax = area.position + area.size;

            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;

            if (_ignoreLeft)
            {
                anchorMin.x = 0f;
            }

            if (_ignoreRight)
            {
                anchorMax.x = 1f;
            }

            if (_ignoreTop)
            {
                anchorMax.y = 1f;
            }

            if (_ignoreBottom)
            {
                anchorMin.y = 0f;
            }

            RectTransform.anchoredPosition = Vector2.zero;
            RectTransform.sizeDelta = Vector2.zero;
            RectTransform.anchorMin = anchorMin;
            RectTransform.anchorMax = anchorMax;

            _currentArea = area;
            
            //BroadcastMessage(UIIgnoreSafeArea.ON_SAFE_AREA_CHANGED, SendMessageOptions.DontRequireReceiver);
        }
    }
}