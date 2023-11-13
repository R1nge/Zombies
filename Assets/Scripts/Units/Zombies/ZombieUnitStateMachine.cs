﻿using System.Collections.Generic;
using Game.Services;
using Units.States;
using Units.Zombies.States;
using UnityEngine;

namespace Units.Zombies
{
    public class ZombieUnitStateMachine
    {
        private readonly Dictionary<ZombieUnitStates, IUnitState> _unitStates;
        private IUnitState _currentState;
        private ZombieUnitStates _currentStateType;

        public ZombieUnitStates CurrentStateType => _currentStateType;

        public ZombieUnitStateMachine(CoroutineRunner coroutineRunner, UnitMovement unitMovement, UnitAnimator unitAnimator, ZombieDeathController zombieDeathController)
        {
            _unitStates = new Dictionary<ZombieUnitStates, IUnitState>
            {
                { ZombieUnitStates.Idle, new ZombieUnitIdleState(unitMovement) },
                { ZombieUnitStates.StandUp, new ZombieUnitStandUpState(coroutineRunner, unitMovement, unitAnimator, this) }, 
                { ZombieUnitStates.Walking, new ZombieUnitMoveState(unitMovement, unitAnimator) },
                { ZombieUnitStates.Infecting, new ZombieUnitInfectState(coroutineRunner, unitMovement, unitAnimator, this) },
                { ZombieUnitStates.Dead, new ZombieUnitDeadState(zombieDeathController) }
            };
        }

        public void SetState(ZombieUnitStates newState)
        {
            if (_unitStates[newState] == _currentState)
            {
                Debug.LogWarning($"Already in {_currentState}");
                return;
            }
            
            _currentStateType = newState;
            
            _currentState?.Exit();
            _currentState = _unitStates[newState];
            _currentState.Enter();
        }

        public void Update() => _currentState?.Update();

        public enum ZombieUnitStates
        {
            Idle,
            StandUp,
            Walking,
            Infecting,
            Dead
        }
    }
}