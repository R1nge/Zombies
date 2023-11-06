using Units.States;
using UnityEngine;

namespace Units.Zombies.States
{
    public class ZombieUnitIdleState : IUnitState
    {
        private readonly Transform _transform;
        private readonly UnitMovement _unitMovement;

        public ZombieUnitIdleState(Transform transform, UnitMovement unitMovement)
        {
            _transform = transform;
            _unitMovement = unitMovement;
        }

        public void Enter()
        {
            _unitMovement.Stop();
        }

        public void Update() { }

        public void Exit() { }
    }
}