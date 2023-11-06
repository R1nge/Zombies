using Units.States;

namespace Units.Humans.Military.States
{
    public class MilitaryUnitChaseState : IUnitState
    {
        private readonly UnitMovement _unitMovement;
        private readonly MilitaryUnitStateMachine _militaryUnitStateMachine;

        public MilitaryUnitChaseState(UnitMovement unitMovement, MilitaryUnitStateMachine militaryUnitStateMachine)
        {
            _unitMovement = unitMovement;
            _militaryUnitStateMachine = militaryUnitStateMachine;
        }

        public void Enter() { }

        public void Update()
        {
            if (_unitMovement.DistanceToDestination() < 5f)
            {
                _militaryUnitStateMachine.SetState(MilitaryUnitStateMachine.MilitaryUnitStates.Attack);
                _unitMovement.Stop();
            }
            else
            {
                _unitMovement.MoveToDestination();
            }
        }

        public void Exit() { }
    }
}