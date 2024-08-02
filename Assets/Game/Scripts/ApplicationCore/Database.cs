using Game.Scripts.LevelsSystem.Levels;
using UnityEngine;

namespace Game.Scripts.ApplicationCore
{
    [CreateAssetMenu(fileName = "Database", menuName = "Game/Database")]
    public class Database : ScriptableObject
    {
        [field: SerializeField] public LevelsCollection LevelsCollection { get; set; }
    }
}