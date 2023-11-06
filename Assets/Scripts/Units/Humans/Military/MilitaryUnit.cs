using Units.Zombies;

namespace Units.Humans.Military
{
    public class MilitaryUnit : Unit
    {
        private MilitaryUnitStateMachine _militaryUnitStateMachine;

        private MilitaryUnitStateMachine.MilitaryUnitStates CurrentState => _militaryUnitStateMachine.CurrentStateType;

        protected override void Awake()
        {
            base.Awake();
            _militaryUnitStateMachine = new MilitaryUnitStateMachine(CoroutineRunner, transform, UnitMovement, UnitAnimator, unitConfig, UnitFactory);
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

            // if (CurrentState == MilitaryUnitStateMachine.MilitaryUnitStates.Chase)
            // {
            //     return;
            // }
            //
            // if (CurrentState == MilitaryUnitStateMachine.MilitaryUnitStates.Dead)
            // {
            //     return;
            // }
            //
            // if (CurrentState == MilitaryUnitStateMachine.MilitaryUnitStates.TurningIntoZombie)
            // {
            //     return;
            // }

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

        public override bool CanBeAttackedBy(Unit unit)
        {
            return CurrentState is not (MilitaryUnitStateMachine.MilitaryUnitStates.Dead or MilitaryUnitStateMachine.MilitaryUnitStates.TurningIntoZombie);
        }
    }
}