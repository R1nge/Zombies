using Units.States;

namespace Units.Humans.Military.States
{
    public class MilitaryUnitPatrolState : IUnitState
    {
        private readonly UnitMovement _unitMovement;
        private readonly UnitPatrolling _unitPatrolling;
        
        public MilitaryUnitPatrolState(UnitMovement unitMovement, UnitPatrolling unitPatrolling)
        {
            _unitMovement = unitMovement;
            _unitPatrolling = unitPatrolling;
        }
        
        public void Enter()
        {
            _unitMovement.MoveToDestination();
        }

        public void Update()
        {
            _unitPatrolling.Patrol();
        }

        public void Exit()
        {
        }
    }
}