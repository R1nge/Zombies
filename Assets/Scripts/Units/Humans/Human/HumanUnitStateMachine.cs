using System.Collections.Generic;
using Game.Services;
using Game.Services.Factories;
using Units.Humans.Human.States;
using Units.States;
using UnityEngine;

namespace Units.Humans.Human
{
    public class HumanUnitStateMachine : UnitStateMachine<HumanUnitStateMachine.HumanUnitStates>
    {
        public HumanUnitStateMachine(CoroutineRunner coroutineRunner, HumanUnit humanUnit, UnitMovement unitMovement, UnitAnimator unitAnimator, UnitFlee unitFlee, UnitFactory unitFactory, UnitRTSController unitRtsController)
        {
            UnitStates = new Dictionary<HumanUnitStates, IUnitState>
            {
                { HumanUnitStates.Idle, new HumanUnitIdleState() },
                { HumanUnitStates.Patrol, new HumanUnitPatrolState() },
                { HumanUnitStates.Flee, new HumanUnitFleeState(unitMovement, unitFlee) },
                { HumanUnitStates.Dead, new HumanUnitDeadState(coroutineRunner, unitMovement, unitAnimator, this, unitRtsController, humanUnit) },
                { HumanUnitStates.TurningIntoZombie, new UnitTurningIntoZombieState(humanUnit.transform, unitFactory, unitRtsController) }
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