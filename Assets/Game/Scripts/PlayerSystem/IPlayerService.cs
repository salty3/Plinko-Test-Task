using System;
using Game.Scripts.PlayerSystem.Data;

namespace Game.Scripts.PlayerSystem
{
    public interface IPlayerService : IService, IApplicationLifetimeHandler
    {
        public T GetData<T>(Func<PlayerSaveData, T> selector);
        public void SetData(Action<PlayerSaveData> action);
    }
}