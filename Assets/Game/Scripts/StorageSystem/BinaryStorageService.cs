using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Scripts.StorageSystem
{
    //"The storage should be able to save in JSON, player prefs, and base64."
    //Not really understood wdym by "base64" but here is a binary storage.
    public class BinaryStorageService : IStorageService
    {
        private bool _saveInProgress;
        
        public UniTask Initialize(CancellationToken token)
        {
            return default;
        }

        public async UniTask<bool> Save(string key, object data)
        {
            if (_saveInProgress)
            {
                return false;
            }
            
            _saveInProgress = true;
            var path = BuildPath(key);
            await UniTask.RunOnThreadPool(async () =>
            {
                var formatter = new BinaryFormatter();
                await using var fileStream = new FileStream(path, FileMode.OpenOrCreate);
                formatter.Serialize(fileStream, data);
            });
            
            _saveInProgress = false;
            return true;
        }

        public async UniTask<T> Load<T>(string key)
        {
            T data = default;
            var path = BuildPath(key);
            await UniTask.RunOnThreadPool(async () =>
            {
                var formatter = new BinaryFormatter();
                await using var fileStream = new FileStream(path, FileMode.Open);
                if (fileStream.Length == 0)
                {
                    return;
                }
                data = (T) formatter.Deserialize(fileStream);
            });
            
            return data;
        }
        
        private static string BuildPath(string key)
        {
            return Path.Combine(UnityEngine.Application.persistentDataPath, key + ".bin");
        }
    }
}