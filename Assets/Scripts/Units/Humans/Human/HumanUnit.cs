using Game.Services;
using Zenject;

namespace Units.Humans.Human
{
    public class HumanUnit : Unit
    {
        private HumanUnitStateMachine _humanUnitStateMachine;
        private HumanCounter _humanCounter;
        private UnitRTSController _unitRtsController;
        private UnitFlee _unitFlee;

        [Inject]
        private void Inject(HumanCounter humanCounter, UnitRTSController unitRtsController)
        {
            _humanCounter = humanCounter;
            _unitRtsController = unitRtsController;
        }

        protected override void Awake()
        {
            base.Awake();
            _unitFlee = new UnitFlee(this, UnitMovement, transform);
            _humanUnitStateMachine = new HumanUnitStateMachine(CoroutineRunner, this, UnitMovement, UnitAnimator, _unitFlee, UnitFactory, _unitRtsController);
        }

        private void Start() => _humanCounter.Add();

        protected override void Update()
        {
            base.Update();
            _humanUnitStateMachine.Update();
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
            //TODO: create a separate method???
            if (!CanBeAttackedBy(unit)) return;

            _unitFlee.SetTarget(unit.transform);
            _humanUnitStateMachine.SetState(HumanUnitStateMachine.HumanUnitStates.Flee);
        }
    }
}