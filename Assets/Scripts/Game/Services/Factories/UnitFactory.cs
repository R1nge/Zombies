using Units.Humans.Military;
using Units.Zombies;
using UnityEngine;
using Zenject;

namespace Game.Services.Factories
{
    public class UnitFactory
    {
        private readonly DiContainer _diContainer;
        private readonly ConfigProvider _configProvider;

        private UnitFactory(DiContainer diContainer, ConfigProvider configProvider)
        {
            _diContainer = diContainer;
            _configProvider = configProvider;
        }

        public ZombieUnit CreateZombieUnit(Vector3 position, Quaternion rotation, Transform parent)
        {
            return _diContainer.InstantiatePrefabForComponent<ZombieUnit>(_configProvider.ZombiesUnitsConfig.Zombie, position, rotation, parent);
        }
        
        public MilitaryUnit CreateMilitaryUnit(Vector3 position, Quaternion rotation, Transform parent)
        {
            return _diContainer.InstantiatePrefabForComponent<MilitaryUnit>(_configProvider, position, rotation, parent);
        }
    }
}