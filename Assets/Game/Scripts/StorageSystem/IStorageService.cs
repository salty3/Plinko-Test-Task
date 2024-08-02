using Cysharp.Threading.Tasks;

namespace Game.Scripts.StorageSystem
{
    public interface IStorageService : IService
    {
        UniTask<bool> Save(string key, object data);
        UniTask<T> Load<T>(string key);
    }
}