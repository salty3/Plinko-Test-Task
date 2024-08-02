using System;
using UnityEngine;

namespace Tools.Runtime
{
    [Serializable]
    public struct SerializableDateTime
    {
        [SerializeField] private long _ticks;

        private bool _initialized;
        private DateTime _dateTime;

        private SerializableDateTime(DateTime dateTime)
        {
            _ticks = dateTime.Ticks;
            _dateTime = dateTime;
            _initialized = true;
        }
        
        public static implicit operator DateTime(SerializableDateTime dateTime)
        {
            if (dateTime._initialized)
            {
                return dateTime._dateTime;
            }

            dateTime._dateTime = new DateTime(dateTime._ticks);
            dateTime._initialized = true;

            return dateTime._dateTime;
        }
        
        public static implicit operator SerializableDateTime(DateTime dateTime)
        {
            return new SerializableDateTime(dateTime);
        }
    }
}