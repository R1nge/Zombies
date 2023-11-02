﻿using System.Collections.Generic;

namespace Units.States
{
    public class UnitStateMachine
    {
        private readonly Dictionary<UnitStates, IUnitState> _unitStates;
        private IUnitState _currentState;

        public UnitStateMachine(UnitMovement unitMovement)
        {
            _unitStates = new Dictionary<UnitStates, IUnitState>
            {
                { UnitStates.Walking, new UnitMoveState(unitMovement) }
            };
        }

        public void SetState(UnitStates newState)
        {
            _currentState?.Exit();
            _currentState = _unitStates[newState];
            _currentState.Enter();
        }

        public void Update() => _currentState?.Update();

        public enum UnitStates
        {
            Idle,
            Walking,
            Infecting,
            Dead
        }
    }
}