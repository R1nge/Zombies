using Units.States;

namespace Units.Humans.Military.States
{
    public class MilitaryUnitChaseState : IUnitState
    {
        private readonly MilitaryUnit _militaryUnit;
        private readonly UnitMovement _unitMovement;
        private readonly MilitaryUnitStateMachine _militaryUnitStateMachine;

        public MilitaryUnitChaseState(MilitaryUnit militaryUnit, UnitMovement unitMovement, MilitaryUnitStateMachine militaryUnitStateMachine)
        {
            _militaryUnit = militaryUnit;
            _unitMovement = unitMovement;
            _militaryUnitStateMachine = militaryUnitStateMachine;
        }

        public void Enter() { }

        public void Update()
        {
            if (_unitMovement.DistanceToDestination() < _militaryUnit.AttackDistance)
            {
                _militaryUnitStateMachine.SetState(MilitaryUnitStateMachine.MilitaryUnitStates.Attack);
                //_unitMovement.Stop();
            }
            else
            {
                _unitMovement.MoveToDestination();
            }
        }

        public void Exit() { }
    }
}