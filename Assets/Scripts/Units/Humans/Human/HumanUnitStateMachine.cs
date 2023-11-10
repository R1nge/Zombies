using System.Collections.Generic;
using Data;
using Game.Services;
using Game.Services.Factories;
using Units.Humans.Human.States;
using Units.States;
using UnityEngine;

namespace Units.Humans.Human
{
    public class HumanUnitStateMachine : UnitStateMachine<HumanUnitStateMachine.HumanUnitStates>
    {
        public HumanUnitStateMachine(CoroutineRunner coroutineRunner, Transform transform, UnitMovement unitMovement, UnitAnimator unitAnimator, UnitFlee unitFlee, UnitFactory unitFactory)
        {
            _unitStates = new Dictionary<HumanUnitStates, IUnitState>
            {
                { HumanUnitStates.Idle, new HumanUnitIdleState() },
                { HumanUnitStates.Patrol, new HumanUnitPatrolState() },
                { HumanUnitStates.Flee, new HumanUnitFleeState(unitMovement, unitFlee) },
                { HumanUnitStates.Dead, new HumanUnitDeadState(coroutineRunner, unitMovement, unitAnimator, this) },
                { HumanUnitStates.TurningIntoZombie, new UnitTurningIntoZombieState(transform, unitFactory) }
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