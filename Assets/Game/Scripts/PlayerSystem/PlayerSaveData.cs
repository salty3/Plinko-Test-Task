using System;
using Game.Scripts.ApplicationCore;

namespace Game.Scripts.PlayerSystem
{
    //If we want to do something on backend side with this - make copy of this class in backend project
    [Serializable]
    public class PlayerSaveData : IDeepCloneable<PlayerSaveData>
    {
        public PlayerSaveData DeepClone()
        {
            return new PlayerSaveData
            {
                
            };
        }
    }
}