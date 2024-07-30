using System;
using Cysharp.Threading.Tasks;

namespace Tools.SceneManagement.Runtime
{
    /// <summary>
    /// Should be placed on scene root object.
    /// </summary>
    public interface ISceneEntryPoint
    {
        UniTask OnSceneOpen(IProgress<float> progress);
    }
}