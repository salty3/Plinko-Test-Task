using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Zenject;

namespace Game.Scripts.TimerSystem
{
    public class TimerService : ITimerService
    {
        private readonly List<Timer> _timers = new(10);
        
        public Timer CreateTimer()
        {
            return CreateTimer(TimeSpan.Zero);
        }

        public Timer CreateTimer(TimeSpan elapsedTime)
        {
            var timer = new Timer(elapsedTime);
            _timers.Add(timer);
            timer.Stopped += () => OnTimerStopped(timer);
            return timer;
        }

        void ITickable.Tick()
        {
            for (var i = 0; i < _timers.Count; i++)
            {
                _timers[i].Tick();
            }
        }
        
        private void OnTimerStopped(Timer timer)
        {
            _timers.Remove(timer);
        }

        public UniTask Initialize(CancellationToken token)
        {
            return UniTask.CompletedTask;
        }
    }
}