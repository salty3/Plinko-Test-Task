using System;
using System.Collections.Generic;
using Game.Scripts.LevelsSystem.Levels;
using UnityEngine;

namespace Game.Scripts.PlayerSystem
{
    //If we want to do something on backend side with this - do copy of this class in backend project
    [Serializable]
    public class PlayerSaveData
    {
        [field: SerializeField] public List<LevelEntity.SaveData> LevelsSaveData { get; set; } = new();
    }
}