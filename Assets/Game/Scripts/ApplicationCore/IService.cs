using System.Threading;
using Cysharp.Threading.Tasks;

namespace Game.Scripts.ApplicationCore
{
    public interface IService
    {
        UniTask Initialize(CancellationToken token);
    }
}