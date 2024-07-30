﻿using UnityEngine;

namespace Game.Scripts.Gameplay
{
    [CreateAssetMenu(fileName = "LevelsCollection", menuName = "Game/LevelsCollection")]
    public class LevelsCollection : ScriptableObject
    {
        //Further can be improved with addressables/bundles. But its unnecessary now
        [SerializeField] private LevelData[] _levels;
    }
}