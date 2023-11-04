using Units.States;

namespace Units.Zombies.States
{
    public class ZombieUnitMoveState : IUnitState
    {
        private readonly UnitMovement _unitMovement;
        private readonly UnitAnimator _unitAnimator;

        public ZombieUnitMoveState(UnitMovement unitMovement, UnitAnimator unitAnimator)
        {
            _unitMovement = unitMovement;
            _unitAnimator = unitAnimator;
        }

        public void Enter() => _unitMovement.MoveToDestination();

        public void Update() => _unitAnimator.PlayRunAnimation(_unitMovement.CurrentSpeed);

        public void Exit()
        {
            _unitMovement.Stop();
            _unitAnimator.PlayRunAnimation(0);
        }
    }
}