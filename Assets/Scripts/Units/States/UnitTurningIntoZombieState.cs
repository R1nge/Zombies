using Data;
using Game.Services.Factories;
using UnityEngine;

namespace Units.States
{
    public class UnitTurningIntoZombieState : IUnitState
    {
        private readonly Transform _transform;
        private readonly UnitFactory _unitFactory;

        public UnitTurningIntoZombieState(Transform transform, UnitFactory unitFactory)
        {
            _transform = transform;
            _unitFactory = unitFactory;
        }

        public void Enter()
        {
            var zombie = _unitFactory.CreateZombieUnit(_transform.position, _transform.rotation, null);
            zombie.StandUp();
            Object.Destroy(_transform.gameObject);
        }

        public void Update() { }

        public void Exit() { }
    }
}