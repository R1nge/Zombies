using Units.States;

namespace Units.Zombies.States
{
    public class ZombieUnitMoveState : IUnitState
    {
        private readonly UnitMovement _unitMovement;

        public ZombieUnitMoveState(UnitMovement unitMovement) => _unitMovement = unitMovement;

        public void Enter() => _unitMovement.MoveToDestination();

        public void Update() { }

        public void Exit() => _unitMovement.Stop();
    }
}