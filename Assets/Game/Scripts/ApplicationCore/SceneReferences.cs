using Tools.SceneManagement.Runtime;
using UnityEngine;

namespace Game.Scripts
{
    [CreateAssetMenu(fileName = "SceneReferences", menuName = "Game/Scene References")]
    public class SceneReferences : ScriptableObject
    {
        [field: SerializeField] public SceneReference Startup { get; private set; }
        [field: SerializeField] public SceneReference Bootstrap { get; private set; }
        [field: SerializeField] public SceneReference GameScene { get; private set; }
        [field: SerializeField] public SceneReference MainMenu { get; private set; }
        [field: SerializeField] public SceneReference Loading { get; private set; }
    }
}