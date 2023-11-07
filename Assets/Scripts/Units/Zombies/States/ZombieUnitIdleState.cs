using Units.States;

namespace Units.Zombies.States
{
    public class ZombieUnitIdleState : IUnitState
    {
        private readonly UnitMovement _unitMovement;

        public ZombieUnitIdleState(UnitMovement unitMovement) => _unitMovement = unitMovement;

        public void Enter() => _unitMovement.Stop();

        public void Update() { }

        public void Exit() { }
    }
}