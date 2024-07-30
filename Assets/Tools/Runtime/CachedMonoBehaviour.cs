using System.Threading;
using UnityEngine;

namespace Tools.Runtime {
    public class CachedMonoBehaviour : MonoBehaviour {
        private Transform _transform;
        private GameObject _gameObject;
#if !UNITY_2022_3_OR_NEWER
        private CancellationToken? _cancellationToken;
#endif
        
        public Transform Transform {
            get
            {
                if (!_transform)
                {
                    _transform = transform;
                }
                return _transform;
            }
        }
        
        public GameObject GameObject {
            get 
            {
                if (!_gameObject)
                {
                    _gameObject = gameObject;
                }
                return _gameObject;
            }
        }

        public CancellationToken DestroyCancellationToken =>
#if UNITY_2022_3_OR_NEWER
            destroyCancellationToken;
#else
            _cancellationToken ??=  Cysharp.Threading.Tasks.UniTaskCancellationExtensions.GetCancellationTokenOnDestroy(GameObject);
#endif
    }
}