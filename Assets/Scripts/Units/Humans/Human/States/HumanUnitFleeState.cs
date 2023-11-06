using Units.States;

namespace Units.Humans.Human.States
{
    public class HumanUnitFleeState : IUnitState
    {
        private readonly UnitFlee _unitFlee;
        private readonly UnitMovement _unitMovement;

        public HumanUnitFleeState(UnitMovement unitMovement, UnitFlee unitFlee)
        {
            _unitMovement = unitMovement;
            _unitFlee = unitFlee;
        }
        
        public void Enter() => _unitMovement.MoveToDestination();

        public void Update() => _unitFlee.Flee();

        public void Exit() => _unitMovement.Stop();
    }
}