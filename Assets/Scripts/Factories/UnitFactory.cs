using Units.Humans;
using Units.Zombies;
using UnityEngine;
using Zenject;

namespace Factories
{
    public class UnitFactory
    {
        private readonly DiContainer _diContainer;

        private UnitFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public ZombieUnit CreateUnit(ZombieUnit unit, Vector3 position, Quaternion rotation, Transform parent)
        {
            return _diContainer.InstantiatePrefabForComponent<ZombieUnit>(unit, position, rotation, parent);
        }
        
        public HumanUnit CreateUnit(HumanUnit unit, Vector3 position, Quaternion rotation, Transform parent)
        {
            return _diContainer.InstantiatePrefabForComponent<HumanUnit>(unit, position, rotation, parent);
        }
    }
}