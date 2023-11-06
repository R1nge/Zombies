using Units.States;
using UnityEngine;

namespace Units.Zombies.States
{
    public class ZombieUnitDeadState : IUnitState
    {
        private readonly UnitAnimator _unitAnimator;
        private readonly ZombieUnit _zombieUnit;
        private readonly UnitMovement _unitMovement;

        public ZombieUnitDeadState(UnitMovement unitMovement, UnitAnimator unitAnimator, ZombieUnit zombieUnit)
        {
            _unitMovement = unitMovement;
            _unitAnimator = unitAnimator;
            _zombieUnit = zombieUnit;
        }

        public void Enter()
        {
            _unitMovement.Stop();
            _unitAnimator.ApplyRootMotion(true);
            _unitAnimator.PlayDeathAnimation();
            Object.Destroy(_zombieUnit);
        }

        public void Update() { }

        public void Exit() { }
    }
}