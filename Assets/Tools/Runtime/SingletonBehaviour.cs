using UnityEngine;

namespace Tools.Runtime
{
    public class SingletonBehaviour<T> : CachedMonoBehaviour
        where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    var instance = new GameObject($"[{typeof(T).Name}]");
                    _instance = instance.AddComponent<T>();
                    DontDestroyOnLoad(instance);
                }

                return _instance;
            }
        }
    }
}