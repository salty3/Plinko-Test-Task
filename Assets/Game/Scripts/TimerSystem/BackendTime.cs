using System;
using Newtonsoft.Json;

namespace Game.Scripts.TimerSystem
{
    // Should be matched with backend to avoid time cheating. Date time wrapper for now.
    [JsonObject]
    public struct BackendTime : IEquatable<BackendTime>
    {
        [JsonProperty]
        private DateTime _time;
        
        public static BackendTime Now => DateTime.Now;
        public static BackendTime Unknown => DateTime.MinValue;
        
        public BackendTime(DateTime time)
        {
            _time = time;
        }

        public static TimeSpan operator -(BackendTime time1, BackendTime time2)
        {
            return time1._time - time2._time;
        }
        
        public static BackendTime operator +(BackendTime time1, TimeSpan time2)
        {
            return new BackendTime(time1._time + time2);
        }
        
        public static bool operator <=(BackendTime a, BackendTime b)
        {
            return a._time <= b._time;
        }
        
        public static bool operator >=(BackendTime a, BackendTime b)
        {
            return a._time >= b._time;
        }
        
        public static bool operator <(BackendTime a, BackendTime b)
        {
            return a._time < b._time;
        }
        
        public static bool operator >(BackendTime a, BackendTime b)
        {
            return a._time > b._time;
        }

        public static bool operator !=(BackendTime a, BackendTime b)
        {
            return a._time != b._time;
        }

        public static bool operator ==(BackendTime a, BackendTime b)
        {
            return a._time == b._time;
        }

        public static implicit operator DateTime(BackendTime time)
        {
            return time._time;
        }

        public static implicit operator BackendTime(DateTime time)
        {
            return new BackendTime(time);
        }
        public bool Equals(BackendTime other)
        {
            return _time.Equals(other._time);
        }

        public override bool Equals(object obj)
        {
            return obj is BackendTime other && Equals(other);
        }

        public override int GetHashCode()
        {
            return _time.GetHashCode();
        }
    }
}