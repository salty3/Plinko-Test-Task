using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Game.Scripts.TimerSystem
{
    public class TimerService : ITimerService
    {
        private readonly List<Timer> _timers = new();
        
        public Timer CreateTimer()
        {
            var timer = new Timer();
            _timers.Add(timer);
            return timer;
        }

        public Timer CreateTimer(BackendTime startTime, TimeSpan duration)
        {
            var timer = new Timer(startTime, duration);
            _timers.Add(timer);
            return timer;
        }

        public void OnEverySecondTick()
        {
            for (var i = 0; i < _timers.Count; i++)
            {
                _timers[i].OnEverySecondTick();
            }
        }

        public UniTask Initialize(CancellationToken token)
        {
            return UniTask.CompletedTask;
        }
    }
}