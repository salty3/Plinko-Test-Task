using System;
using Zenject;

namespace Game.Scripts.TimerSystem
{
    public class Timer : ITickable, IDisposable
    {
        public TimeSpan ElapsedTime { get; private set; }
        public bool IsRunning { get; private set; }
        public event Action Updated;
        public event Action Stopped;
        
        private BackendTime _lastTickTime;

        public Timer(TimeSpan elapsedTime)
        {
            ElapsedTime = elapsedTime;
        }
        
        public void Tick()
        {
            if (!IsRunning)
            {
                return;
            }
            
            ElapsedTime += BackendTime.Now - _lastTickTime;
            _lastTickTime = BackendTime.Now;
            Updated?.Invoke();
        }

        public void Dispose()
        {
            IsRunning = false;
            Stopped?.Invoke();
            Stopped = null;
        }

        public void Start()
        {
            _lastTickTime = BackendTime.Now;
            IsRunning = true;
        }
        
        public void Pause()
        {
            IsRunning = false;
        }
    }
}