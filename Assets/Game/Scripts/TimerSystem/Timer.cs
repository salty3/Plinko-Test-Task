using System;

namespace Game.Scripts.TimerSystem
{
    public class Timer : IReadOnlyTimer, IEverySecondTickable
    {
        public TimeSpan RemainingTime { get; private set; } = TimeSpan.Zero;
        public TimeSpan ElapsedTime { get; private set; } = TimeSpan.Zero;
        public bool IsEnded { get; private set; } = false;
        public bool IsRunning { get; private set; } = false;

        private BackendTime _startTime;
        private TimeSpan _duration;

        public Timer()
        {
            
        }
        
        public Timer(BackendTime startTime, TimeSpan duration)
        {
            SetDuration(duration);
            _startTime = startTime;
            if (_startTime != BackendTime.Unknown)
            {
                IsRunning = true;
            }
        }

        public void Start()
        {
            if (IsRunning)
            {
                return;
            }

            _startTime = BackendTime.Now;
            IsRunning = true;
        }

        public void Reset()
        {
            _startTime = BackendTime.Unknown;
            RemainingTime = TimeSpan.Zero;
            ElapsedTime = TimeSpan.Zero;
            IsEnded = false;
            IsRunning = false;
        }
        
        public void SetDuration(TimeSpan duration)
        {
            if (IsEnded)
            {
                return;
            }
            
            _duration = duration;

            if (IsRunning)
            {
                return;
            }
            
            RemainingTime = duration;
        }
        
        public void OnEverySecondTick()
        {
            if (!IsRunning || IsEnded)
            {
                return;
            }
            
            ElapsedTime = BackendTime.Now - _startTime;
            RemainingTime = _duration - ElapsedTime;
            
            if (RemainingTime <= TimeSpan.Zero)
            {
                OnTimerEnd();
            }
        }
        
        private void OnTimerEnd()
        {
            IsEnded = true;
            IsRunning = false;
        }
    }
}