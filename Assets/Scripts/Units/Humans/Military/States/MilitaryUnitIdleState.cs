using Units.States;

namespace Units.Humans.Military.States
{
    public class MilitaryUnitIdleState : IUnitState
    {
        private readonly MilitaryUnit _militaryUnit;
        private readonly UnitMovement _unitMovement;

        public MilitaryUnitIdleState(MilitaryUnit militaryUnit, UnitMovement unitMovement)
        {
            _militaryUnit = militaryUnit;
            _unitMovement = unitMovement;
        }
        
        public void Enter()
        {
            _militaryUnit.Patrol();
            _unitMovement.ResetTarget();
        }

        public void Update()
        {
            
        }

        public void Exit()
        {
        }
    }
}