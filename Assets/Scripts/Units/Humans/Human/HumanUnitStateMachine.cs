using System.Collections.Generic;
using Data;
using Factories;
using Game.Services;
using Units.Humans.Human.States;
using Units.States;
using UnityEngine;

namespace Units.Humans.Human
{
    public class HumanUnitStateMachine : UnitStateMachine<HumanUnitStateMachine.HumanUnitStates>
    {
        public HumanUnitStateMachine(CoroutineRunner coroutineRunner, UnitAnimator unitAnimator, Transform transform, UnitConfig unitConfig, UnitFactory unitFactory)
        {
            _unitStates = new Dictionary<HumanUnitStates, IUnitState>
            {
                { HumanUnitStates.Idle, new HumanUnitIdleState() },
                { HumanUnitStates.Patrol, new HumanUnitPatrolState() },
                { HumanUnitStates.Flee, new HumanUnitFleeState() },
                { HumanUnitStates.Dead, new HumanUnitDeadState(coroutineRunner, unitAnimator, this) },
                { HumanUnitStates.TurningIntoZombie, new UnitTurningIntoZombieState(transform, unitConfig, unitFactory) }
            };
        }
        
        
        public enum HumanUnitStates
        {
            Idle,
            Patrol,
            Flee,
            Dead,
            TurningIntoZombie
        }
    }
}