using Game.Scripts.Gameplay;
using UnityEngine;

namespace Game.Scripts
{
    [CreateAssetMenu(fileName = "Database", menuName = "Game/Database")]
    public class Database : ScriptableObject
    {
        [field: SerializeField] public LevelsCollection LevelsCollection { get; set; }
    }
}