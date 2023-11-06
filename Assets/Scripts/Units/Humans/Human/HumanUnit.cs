namespace Units.Humans.Human
{
    public class HumanUnit : Unit
    {
        private HumanUnitStateMachine _humanUnitStateMachine;

        protected override void Awake()
        {
            base.Awake();
            _humanUnitStateMachine = new HumanUnitStateMachine(CoroutineRunner, UnitAnimator, transform, unitConfig, UnitFactory);
        }

        public override void Idle()
        {
            _humanUnitStateMachine.SetState(HumanUnitStateMachine.HumanUnitStates.Idle);
        }

        public override void Die()
        {
            _humanUnitStateMachine.SetState(HumanUnitStateMachine.HumanUnitStates.Dead);
        }

        public override bool CanBeAttacked()
        {
            return _humanUnitStateMachine.CurrentStateType is not (HumanUnitStateMachine.HumanUnitStates.Dead
                or HumanUnitStateMachine.HumanUnitStates.TurningIntoZombie);
        }

        public void Flee()
        {
        }
    }
}