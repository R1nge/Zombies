using System;
using Data;
using Units.Zombies;
using UnityEngine;

namespace Units.Humans
{
    public class HumanUnit : Unit
    {
        [SerializeField] private HumanConfig humanConfig;
        private HumanUnitStateMachine _humanUnitStateMachine;

        public HumanUnitStateMachine.HumanUnitStates CurrentState => _humanUnitStateMachine.CurrentStateType;

        protected override void Awake()
        {
            base.Awake();
            NavMeshAgent.speed = humanConfig.Speed;
            _humanUnitStateMachine = new HumanUnitStateMachine(CoroutineRunner, UnitAnimator, this, humanConfig, UnitFactory);
        }

        protected override void Update()
        {
            base.Update();
            _humanUnitStateMachine.Update();
        }

        public override void Idle() { }

        public void Chase(ZombieUnit zombieUnit)
        {
            if (CurrentState is HumanUnitStateMachine.HumanUnitStates.Idle
                or HumanUnitStateMachine.HumanUnitStates.Patrol)
            {
                //_humanUnitStateMachine.SetState(HumanUnitStateMachine.HumanUnitStates.Chase);
            }
        }

        public override void StandUp() { }
        public override void Attack() { }
        public override void Die() => _humanUnitStateMachine.SetState(HumanUnitStateMachine.HumanUnitStates.Dead);
    }
}