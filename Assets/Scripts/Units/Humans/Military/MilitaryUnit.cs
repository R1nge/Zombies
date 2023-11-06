using Data;
using Units.Zombies;
using UnityEngine;

namespace Units.Humans.Military
{
    public class MilitaryUnit : Unit
    {
        private MilitaryUnitStateMachine _militaryUnitStateMachine;

        private MilitaryUnitStateMachine.MilitaryUnitStates CurrentState => _militaryUnitStateMachine.CurrentStateType;

        protected override void Awake()
        {
            base.Awake();
            NavMeshAgent.speed = unitConfig.Speed;
            _militaryUnitStateMachine = new MilitaryUnitStateMachine(CoroutineRunner, transform, UnitAnimator, unitConfig, UnitFactory);
        }

        protected override void Update()
        {
            base.Update();
            _militaryUnitStateMachine.Update();
        }

        public override void Idle()
        {
        }

        public void Chase(ZombieUnit zombieUnit)
        {
            UnitMovement.SetDestination(zombieUnit.transform.position);

            if (CurrentState == MilitaryUnitStateMachine.MilitaryUnitStates.Chase)
            {
                return;
            }

            if (CurrentState == MilitaryUnitStateMachine.MilitaryUnitStates.Dead)
            {
                return;
            }

            if (CurrentState == MilitaryUnitStateMachine.MilitaryUnitStates.TurningIntoZombie)
            {
                return;
            }

            _militaryUnitStateMachine.SetState(MilitaryUnitStateMachine.MilitaryUnitStates.Chase);
        }

        public void StandUp()
        {
        }

        public void Attack()
        {
        }

        public override void Die()
        {
            _militaryUnitStateMachine.SetState(MilitaryUnitStateMachine.MilitaryUnitStates.Dead);
        }

        public override bool CanBeAttacked()
        {
            return CurrentState is not (MilitaryUnitStateMachine.MilitaryUnitStates.Dead or MilitaryUnitStateMachine.MilitaryUnitStates.TurningIntoZombie);
        }
    }
}