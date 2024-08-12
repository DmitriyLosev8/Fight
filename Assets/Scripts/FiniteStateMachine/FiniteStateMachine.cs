using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.FiniteStateMachine
{
    internal  class FiniteStateMachine
    {
        private FSMState _currentState;
        private Dictionary<Type, FSMState> _states = new Dictionary<Type, FSMState>();

        public void AddState(FSMState state)
        {
            _states.Add(state.GetType(), state);
        }

        public void SetState<T>()
            where T : FSMState
        {
            var type = typeof(T);
 
            if(_currentState != null && _currentState.GetType() == type)
            {
                return;
            }
            
            if (_states.TryGetValue(type, out var newState))
            {
                _currentState?.Exit();

                _currentState = newState;

                _currentState.Enter();
            }
        }

        public void Update()
        {
            _currentState?.Update();
        }
    }
}