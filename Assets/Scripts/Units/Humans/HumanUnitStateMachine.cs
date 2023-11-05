using System.Collections.Generic;
using Data;
using Factories;
using Units.Humans.States;
using Units.States;
using Units.Zombies;
using UnityEngine;

namespace Units.Humans
{
    public class HumanUnitStateMachine
    {
        private readonly Dictionary<HumanUnitStates, IUnitState> _unitStates;
        private IUnitState _currentState;
        private HumanUnitStates _currentStateType;

        public HumanUnitStates CurrentStateType => _currentStateType;

        public HumanUnitStateMachine(CoroutineRunner coroutineRunner, UnitAnimator unitAnimator, HumanUnit humanUnit, HumanConfig humanConfig, UnitFactory unitFactory)
        {
            _unitStates = new Dictionary<HumanUnitStates, IUnitState>
            {
                { HumanUnitStates.Idle, new HumanUnitIdle() },
                { HumanUnitStates.Dead, new HumanUnitDead(coroutineRunner, unitAnimator, this) },
                { HumanUnitStates.TurningIntoZombie, new HumanUnitTurningIntoZombie(humanUnit.transform, humanConfig, unitFactory) }
            };
        }

        public void SetState(HumanUnitStates newState)
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

        public enum HumanUnitStates
        {
            Idle,
            Patrol,
            Chase,
            Attack,
            Dead,
            TurningIntoZombie
        }
    }
}