using System;

namespace Game.Scripts.TimerSystem
{
    public interface IReadOnlyTimer
    {
        TimeSpan RemainingTime { get; }
        TimeSpan ElapsedTime { get; }
        bool IsEnded { get; }
        bool IsRunning { get; }
    }
}