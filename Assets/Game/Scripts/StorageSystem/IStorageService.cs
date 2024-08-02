using Cysharp.Threading.Tasks;
using Game.Scripts.ApplicationCore;

namespace Game.Scripts.StorageSystem
{
    public interface IStorageService : IService
    {
        UniTask<bool> Save(string key, object data);
        UniTask<T> Load<T>(string key);
    }
}