using System.Collections.Generic;
using Data;
using Factories;
using Game.Services;
using Units.Humans.Military.States;
using Units.States;
using UnityEngine;

namespace Units.Humans.Military
{
    public class MilitaryUnitStateMachine : UnitStateMachine<MilitaryUnitStateMachine.MilitaryUnitStates>
    {
        public MilitaryUnitStateMachine(CoroutineRunner coroutineRunner, Transform transform, UnitMovement unitMovement, UnitAnimator unitAnimator, UnitConfig unitConfig, UnitFactory unitFactory)
        {
            _unitStates = new Dictionary<MilitaryUnitStates, IUnitState>
            {
                { MilitaryUnitStates.Idle, new MilitaryUnitIdleState(unitMovement) },
                { MilitaryUnitStates.Patrol, new MilitaryUnitPatrolState() },
                { MilitaryUnitStates.Chase, new MilitaryUnitChaseState(unitMovement, this) },
                { MilitaryUnitStates.Attack, new MilitaryUnitAttackState(transform, unitMovement, this) },
                { MilitaryUnitStates.Dead, new MilitaryUnitDeadState(coroutineRunner, unitMovement, unitAnimator, this) },
                { MilitaryUnitStates.TurningIntoZombie, new UnitTurningIntoZombieState(transform, unitConfig, unitFactory) }
            };
        }

        public enum MilitaryUnitStates
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