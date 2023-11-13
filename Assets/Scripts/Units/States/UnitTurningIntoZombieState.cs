using Game.Services;
using Game.Services.Factories;
using Units.Zombies;
using UnityEngine;

namespace Units.States
{
    public class UnitTurningIntoZombieState : IUnitState
    {
        private readonly Transform _transform;
        private readonly UnitFactory _unitFactory;
        private readonly ZombieCounter _zombieCounter;

        public UnitTurningIntoZombieState(Transform transform, UnitFactory unitFactory, ZombieCounter zombieCounter)
        {
            _transform = transform;
            _unitFactory = unitFactory;
            _zombieCounter = zombieCounter;
        }

        public void Enter()
        {
            ZombieUnit zombie = _unitFactory.CreateZombieUnit(_transform.position, _transform.rotation, null);
            zombie.StandUp();
            _zombieCounter.RemovePending();
            Object.Destroy(_transform.gameObject);
        }

        public void Update() { }

        public void Exit() { }
    }
}