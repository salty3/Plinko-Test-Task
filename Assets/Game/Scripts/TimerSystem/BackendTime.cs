using System;
using Tools.Runtime;
using UnityEngine;

namespace Game.Scripts.TimerSystem
{
    // Should be matched with backend to avoid time cheating. Date time wrapper for now.
    [Serializable]
    public struct BackendTime : IEquatable<BackendTime>
    {
        [SerializeField]
        private SerializableDateTime _time;
        
        public static BackendTime Now => DateTime.Now;
        public static BackendTime Unknown => DateTime.MinValue;
        
        public BackendTime(DateTime time)
        {
            _time = time;
        }

        public static TimeSpan operator -(BackendTime time1, BackendTime time2)
        {
            return (DateTime) time1._time - time2._time;
        }
        
        public static BackendTime operator +(BackendTime time1, TimeSpan time2)
        {
            return new BackendTime((DateTime)time1._time + time2);
        }
        
        public static bool operator <=(BackendTime a, BackendTime b)
        {
            return (DateTime)a._time <= b._time;
        }
        
        public static bool operator >=(BackendTime a, BackendTime b)
        {
            return (DateTime)a._time >= b._time;
        }
        
        public static bool operator <(BackendTime a, BackendTime b)
        {
            return (DateTime)a._time < b._time;
        }
        
        public static bool operator >(BackendTime a, BackendTime b)
        {
            return (DateTime)a._time > b._time;
        }

        public static bool operator !=(BackendTime a, BackendTime b)
        {
            return (DateTime)a._time != b._time;
        }

        public static bool operator ==(BackendTime a, BackendTime b)
        {
            return (DateTime)a._time == b._time;
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