using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Tools.Runtime.StateBehaviour
{
    public abstract class StateManager<T> : IDisposable where T : class, IState
    {
        public readonly UnityEvent<T> StateChanged = new();

        private readonly Dictionary<Type, T> _statesMap;
        private T _state;
        
        private readonly DiContainer _scopedContainer;
        
        public virtual T Current
        {
            get => _state;
            protected set
            {
                _state?.Dispose();
                _state = value;
                _state.Initialize();
                StateChanged.Invoke(_state);
            }
        }
        
        protected StateManager(Type[] types, DiContainer container)
        {
            _statesMap = new Dictionary<Type, T>(types.Length);
            _scopedContainer = container.CreateSubContainer();
            foreach (var type in types)
            {
                _scopedContainer.Bind(type).AsSingle();
            }
            _state = null;
        }

        public void SwitchToState<T1>()
        {
            SwitchToState(typeof(T1));
        }

        public void SwitchToState(Type type)
        {
            if (!_statesMap.ContainsKey(type))
            {
                _statesMap[type] = _scopedContainer.Resolve(type) as T;
            }

            var state = _statesMap[type];
            Current = state;
        }
        
        public void Dispose()
        {
            _state?.Dispose();
            _state = null;
            _statesMap.Clear();
        }
    }
}