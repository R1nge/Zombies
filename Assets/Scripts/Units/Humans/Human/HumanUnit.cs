using Game.Services;
using UnityEngine;
using Zenject;

namespace Units.Humans.Human
{
    public class HumanUnit : Unit
    {
        private HumanUnitStateMachine _humanUnitStateMachine;
        private HumanCounter _humanCounter;

        [Inject]
        private void Inject(HumanCounter humanCounter) => _humanCounter = humanCounter;

        protected override void Awake()
        {
            base.Awake();
            _humanUnitStateMachine = new HumanUnitStateMachine(CoroutineRunner, transform, UnitMovement, UnitAnimator,  unitConfig, UnitFactory);
            _humanCounter.Add();
        }

        public override void Idle()
        {
            _humanUnitStateMachine.SetState(HumanUnitStateMachine.HumanUnitStates.Idle);
        }

        public override void Die()
        {
            _humanUnitStateMachine.SetState(HumanUnitStateMachine.HumanUnitStates.Dead);
            _humanCounter.Remove();
        }

        public override bool CanBeAttackedBy(Unit unit)
        {
            return _humanUnitStateMachine.CurrentStateType is not (HumanUnitStateMachine.HumanUnitStates.Dead
                or HumanUnitStateMachine.HumanUnitStates.TurningIntoZombie);
        }

        public void FleeFrom(Unit unit)
        {
            Vector3 direction = unit.transform.position - transform.position;
            UnitMovement.SetDestination(transform.position - direction.normalized);
            _humanUnitStateMachine.SetState(HumanUnitStateMachine.HumanUnitStates.Flee);
        }
    }
}