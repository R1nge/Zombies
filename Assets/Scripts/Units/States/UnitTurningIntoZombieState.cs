using Game.Services.Factories;
using Units.Zombies;
using UnityEngine;

namespace Units.States
{
    public class UnitTurningIntoZombieState : IUnitState
    {
        private readonly Transform _transform;
        private readonly UnitFactory _unitFactory;
        private readonly UnitRTSController _unitRtsController;

        public UnitTurningIntoZombieState(Transform transform, UnitFactory unitFactory, UnitRTSController unitRtsController)
        {
            _transform = transform;
            _unitFactory = unitFactory;
            _unitRtsController = unitRtsController;
        }

        public void Enter()
        {
            ZombieUnit zombie = _unitFactory.CreateZombieUnit(_transform.position, _transform.rotation, null);
            zombie.StandUp();
            _unitRtsController.RemovePending(_transform.gameObject);
            Object.Destroy(_transform.gameObject);
        }

        public void Update() { }

        public void Exit() { }
    }
}