using Data;
using Factories;
using Units.States;
using Units.Zombies;
using UnityEngine;

namespace Units.Humans.States
{
    public class HumanUnitTurningIntoZombie : IUnitState
    {
        private readonly Transform _transform;
        private readonly HumanConfig _humanConfig;
        private readonly UnitFactory _unitFactory;

        public HumanUnitTurningIntoZombie(Transform transform, HumanConfig humanConfig, UnitFactory unitFactory)
        {
            _transform = transform;
            _humanConfig = humanConfig;
            _unitFactory = unitFactory;
        }

        public void Enter()
        {
            ZombieUnit zombie = _unitFactory.CreateUnit(_humanConfig.Zombie, _transform.position, _transform.rotation, null);
            zombie.StandUp();
            Object.Destroy(_transform.gameObject);
        }

        public void Update() { }

        public void Exit() { }
    }
}