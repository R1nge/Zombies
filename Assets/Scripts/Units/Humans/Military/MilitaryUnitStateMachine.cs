using System.Collections.Generic;
using Data;
using Factories;
using Units.Humans.Military.States;
using Units.States;
using UnityEngine;

namespace Units.Humans.Military
{
    public class MilitaryUnitStateMachine : UnitStateMachine<MilitaryUnitStateMachine.MilitaryUnitStates>
    {
        public MilitaryUnitStateMachine(CoroutineRunner coroutineRunner, Transform transform, UnitAnimator unitAnimator, UnitConfig unitConfig, UnitFactory unitFactory)
        {
            _unitStates = new Dictionary<MilitaryUnitStates, IUnitState>
            {
                { MilitaryUnitStates.Idle, new MilitaryUnitIdleState() },
                { MilitaryUnitStates.Patrol, new MilitaryUnitPatrolState() },
                { MilitaryUnitStates.Chase, new MilitaryUnitChaseState() },
                { MilitaryUnitStates.Attack, new MilitaryUnitAttackState() },
                { MilitaryUnitStates.Dead, new MilitaryUnitDeadState(coroutineRunner, unitAnimator, this) },
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