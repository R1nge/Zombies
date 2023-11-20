using System.Collections.Generic;
using Game.Services;
using Game.Services.Factories;
using Units.Humans.Military.States;
using Units.States;
using UnityEngine;

namespace Units.Humans.Military
{
    public class MilitaryUnitStateMachine : UnitStateMachine<MilitaryUnitStateMachine.MilitaryUnitStates>
    {
        public MilitaryUnitStateMachine(CoroutineRunner coroutineRunner, MilitaryUnit militaryUnit, Transform transform, UnitMovement unitMovement, UnitPatrolling unitPatrolling, UnitAnimator unitAnimator, UnitFactory unitFactory, UnitSoundsController unitSoundsController, Sensor[] sensors, UnitRTSController unitRtsController)
        {
            UnitStates = new Dictionary<MilitaryUnitStates, IUnitState>
            {
                { MilitaryUnitStates.Idle, new MilitaryUnitIdleState(militaryUnit, unitMovement) },
                { MilitaryUnitStates.Patrol, new MilitaryUnitPatrolState(unitMovement, unitPatrolling) },
                { MilitaryUnitStates.Chase, new MilitaryUnitChaseState(militaryUnit,unitMovement, this) },
                { MilitaryUnitStates.Attack, new MilitaryUnitAttackState(militaryUnit, transform, unitMovement, unitSoundsController, this, unitAnimator) },
                { MilitaryUnitStates.Dead, new MilitaryUnitDeadState(coroutineRunner, unitMovement, unitAnimator, this, sensors, unitRtsController, militaryUnit) },
                { MilitaryUnitStates.TurningIntoZombie, new UnitTurningIntoZombieState(transform, unitFactory, unitRtsController) }
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