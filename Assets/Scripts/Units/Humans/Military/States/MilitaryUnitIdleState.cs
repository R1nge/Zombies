using Units.States;

namespace Units.Humans.Military.States
{
    public class MilitaryUnitIdleState : IUnitState
    {
        private readonly MilitaryUnit _militaryUnit;

        public MilitaryUnitIdleState(MilitaryUnit militaryUnit)
        {
            _militaryUnit = militaryUnit;
        }
        
        public void Enter()
        {
            _militaryUnit.Patrol();
        }

        public void Update()
        {
            
        }

        public void Exit()
        {
        }
    }
}