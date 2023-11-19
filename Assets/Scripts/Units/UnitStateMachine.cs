using System;
using System.Collections.Generic;
using Units.States;
using UnityEngine;

namespace Units
{
    public abstract class UnitStateMachine<T> where T : Enum
    {
        protected Dictionary<T, IUnitState> UnitStates;
        private IUnitState _currentState;
        private T _currentStateType;

        public T CurrentStateType => _currentStateType;

        public void SetState(T newState)
        {
            if (UnitStates[newState] == _currentState)
            {
                Debug.LogWarning($"Already in {_currentState}");
                return;
            }

            _currentStateType = newState;

            _currentState?.Exit();
            _currentState = UnitStates[newState];
            _currentState.Enter();
        }

        public void Update() => _currentState?.Update();
    }
}