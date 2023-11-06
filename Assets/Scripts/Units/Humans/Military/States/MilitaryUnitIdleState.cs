using Units.States;

namespace Units.Humans.Military.States
{
    public class MilitaryUnitIdleState : IUnitState
    {
        private readonly UnitMovement _unitMovement;
        
        public MilitaryUnitIdleState(UnitMovement unitMovement)
        {
            _unitMovement = unitMovement;
        }
        
        public void Enter()
        {
            _unitMovement.Stop();
        }

        public void Update()
        {
        }

        public void Exit()
        {
        }
    }
}