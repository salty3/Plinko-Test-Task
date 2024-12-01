using System.Collections.Generic;
using UnityEngine;

namespace Game.PoolingSystem
{
    // Simplest pool implementation
    public class Pool
    {
        private readonly Stack<GameObject> _inactive;
        private readonly GameObject _prefab;

        public Pool(GameObject prefab, int initialSize)
        {
            _prefab = prefab;
            _inactive = new Stack<GameObject>(initialSize);
        }

        public GameObject Get()
        {
            var spawnedObject = _inactive.Count == 0 ? Object.Instantiate(_prefab) : _inactive.Pop();
            spawnedObject.SetActive(true);
            return spawnedObject;
        }
        
        public void Return(GameObject spawnedObject)
        {
            spawnedObject.SetActive(false);
            _inactive.Push(spawnedObject);
        }
    }
}