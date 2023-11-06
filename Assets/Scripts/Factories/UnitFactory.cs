using Units.Humans;
using Units.Humans.Military;
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
        
        public MilitaryUnit CreateUnit(MilitaryUnit unit, Vector3 position, Quaternion rotation, Transform parent)
        {
            return _diContainer.InstantiatePrefabForComponent<MilitaryUnit>(unit, position, rotation, parent);
        }
    }
}