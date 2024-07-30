using System;

namespace Game.Scripts.TimerSystem
{
    public interface ITimerService : IService, IEverySecondTickable
    {
        Timer CreateTimer();
        Timer CreateTimer(BackendTime startTime, TimeSpan duration);
    }
}