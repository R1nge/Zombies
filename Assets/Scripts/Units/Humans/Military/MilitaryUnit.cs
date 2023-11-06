using Units.Zombies;
using Zenject;

namespace Units.Humans.Military
{
    public class MilitaryUnit : Unit
    {
        private MilitaryUnitStateMachine _militaryUnitStateMachine;
        private HumanCounter _humanCounter;

        private MilitaryUnitStateMachine.MilitaryUnitStates CurrentState => _militaryUnitStateMachine.CurrentStateType;

        [Inject]
        private void Inject(HumanCounter humanCounter) => _humanCounter = humanCounter;

        protected override void Awake()
        {
            base.Awake();
            _militaryUnitStateMachine = new MilitaryUnitStateMachine(CoroutineRunner, transform, UnitMovement, UnitAnimator, unitConfig, UnitFactory);
            _humanCounter.Add();
        }

        protected override void Update()
        {
            base.Update();
            _militaryUnitStateMachine.Update();
            print($"MILITARY: Current state {_militaryUnitStateMachine.CurrentStateType}");
        }

        public override void Idle()
        {
        }

        public void Chase(ZombieUnit zombieUnit)
        {
            UnitMovement.SetDestination(zombieUnit.transform.position);
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
            _humanCounter.Remove();
        }

        public override bool CanBeAttackedBy(Unit unit)
        {
            return CurrentState is not (MilitaryUnitStateMachine.MilitaryUnitStates.Dead or MilitaryUnitStateMachine.MilitaryUnitStates.TurningIntoZombie);
        }
    }
}