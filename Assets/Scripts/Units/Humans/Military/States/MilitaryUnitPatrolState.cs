using Units.States;

namespace Units.Humans.Military.States
{
    public class MilitaryUnitPatrolState : IUnitState
    {
        private readonly UnitPatrolling _unitPatrolling;
        
        public MilitaryUnitPatrolState(UnitPatrolling unitPatrolling)
        {
            _unitPatrolling = unitPatrolling;
        }
        
        public void Enter()
        {
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