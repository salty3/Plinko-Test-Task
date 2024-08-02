using System;
using Game.Scripts.ApplicationCore;
using Zenject;

namespace Game.Scripts.TimerSystem
{
    public interface ITimerService : IService, ITickable
    {
        Timer CreateTimer();
        Timer CreateTimer(TimeSpan elapsedTime);
    }
}