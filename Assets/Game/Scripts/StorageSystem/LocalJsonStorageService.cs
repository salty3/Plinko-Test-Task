using System;
using System.IO;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Scripts.StorageSystem
{
    public class LocalJsonStorageService : IStorageService
    {
        private bool _saveInProgress;
        
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
                string json = JsonUtility.ToJson(data);
                //Ideally we should receive encode token from backend and send encoded data to it.
                string encodedJson = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(json));
                await using var fileStream = new FileStream(path, FileMode.OpenOrCreate);
                await using var streamWriter = new StreamWriter(fileStream);
                await streamWriter.WriteAsync(encodedJson);
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
                await using var fileStream = new FileStream(path, FileMode.OpenOrCreate);
                if (fileStream.Length == 0)
                {
                    return;
                }
                using var streamReader = new StreamReader(fileStream);
                string encodedJson = await streamReader.ReadToEndAsync();
                string json = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(encodedJson));
                data = JsonUtility.FromJson<T>(json);
            });

            return data;
        }
        
        private static string BuildPath(string key)
        {
            return Path.Combine(UnityEngine.Application.persistentDataPath, key + ".json");
        }

        public UniTask Initialize(CancellationToken token)
        {
            return default;
        }
    }
}