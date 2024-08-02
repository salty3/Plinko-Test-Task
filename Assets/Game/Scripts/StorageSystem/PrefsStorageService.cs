using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Scripts.StorageSystem
{
    public class PrefsStorageService : IStorageService
    {
        public UniTask<bool> Save(string key, object data)
        {
            string json = JsonUtility.ToJson(data);
            //string encodedJson = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(json));
            PlayerPrefs.SetString(key, json);
            PlayerPrefs.Save();
            return UniTask.FromResult(true);
        }

        public UniTask<T> Load<T>(string key)
        {
            string encodedJson = PlayerPrefs.GetString(key);
            //string json = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(encodedJson));
            var data = JsonUtility.FromJson<T>(encodedJson);
            return UniTask.FromResult(data);
        }

        public UniTask Initialize(CancellationToken token)
        {
            return default;
        }
    }
}