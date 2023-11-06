using Data;
using Factories;
using UnityEngine;

namespace Units.States
{
    public class UnitTurningIntoZombieState : IUnitState
    {
        private readonly Transform _transform;
        private readonly UnitConfig _unitConfig;
        private readonly UnitFactory _unitFactory;

        public UnitTurningIntoZombieState(Transform transform, UnitConfig unitConfig, UnitFactory unitFactory)
        {
            _transform = transform;
            _unitConfig = unitConfig;
            _unitFactory = unitFactory;
        }

        public void Enter()
        {
            var zombie = _unitFactory.CreateUnit(_unitConfig.Zombie, _transform.position, _transform.rotation, null);
            zombie.StandUp();
            Object.Destroy(_transform.gameObject);
        }

        public void Update() { }

        public void Exit() { }
    }
}