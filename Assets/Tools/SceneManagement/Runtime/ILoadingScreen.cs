using Cysharp.Threading.Tasks;

namespace Tools.SceneManagement.Runtime
{
    public interface ILoadingScreen
    {
        UniTask Open();
        UniTask Close();
        void SetProgress(float progress);
    }
}