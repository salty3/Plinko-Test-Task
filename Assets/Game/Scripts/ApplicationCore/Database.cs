using Game.Scripts.BetSystem;
using UnityEngine;

namespace Game.Scripts.ApplicationCore
{
    [CreateAssetMenu(fileName = "Database", menuName = "Game/Database")]
    public class Database : ScriptableObject
    {
        [field: SerializeField] public WinValuesConfig WinValuesConfig { get; private set; }
        [field: SerializeField] public PinRowsConfig PinRowsConfig { get; private set; }
        [field: SerializeField] public BetsConfig BetsConfig { get; private set; }
    }
}