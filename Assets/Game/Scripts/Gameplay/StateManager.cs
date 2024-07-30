using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;
using Zenject;

namespace Game.Scripts.Gameplay
{
    public abstract class StateManager<T> : IDisposable where T : State
    {
        public readonly UnityEvent<T> OnChangeState = new();

        private readonly Dictionary<Type, T> _statesMap;
        private T _state;
        public virtual T Current
        {
            get => _state;
            protected set
            {
                _state?.Dispose();
                _state = value;
                _state.Initialize();
                OnChangeState.Invoke(_state);
            }
        }

        [Inject]
        protected StateManager(IEnumerable<State> states)
        {
            _statesMap = states.ToDictionary(s => s.GetType(), s => (T) s);
            _state = null;
        }

        
        public void SwitchToState(T state)
        {
            Current = state;
        }

        public void SwitchToState<T1>()
        {
            SwitchToState(typeof(T1));
        }

        public void SwitchToState(Type type)
        {
            if (!_statesMap.TryGetValue(type, out var state))
            {
                throw new ArgumentException($"State {type} is not registered");
            }

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