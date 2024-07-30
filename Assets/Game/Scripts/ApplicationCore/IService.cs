using System.Threading;
using Cysharp.Threading.Tasks;

namespace Game.Scripts
{
    public interface IService
    {
        UniTask Initialize(CancellationToken token);
    }
}