using Game;
using Game.Services;
using Game.Services.Factories;
using Units;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private HumanSpawner humanSpawner;
        [SerializeField] private ZombieSpawner zombieSpawner;
        [SerializeField] private CameraService cameraService;
        
        public override void InstallBindings()
        {
            Container.BindInstance(cameraService);
            
            Container.Bind<MarkerPositionService>().AsSingle();
            Container.Bind<HumanCounter>().AsSingle();

            Container.Bind<UnitRTSController>().AsSingle();

            Container.Bind<UIFactory>().AsSingle();
            
            Container.Bind<GameStateFactory>().AsSingle();
            Container.Bind<GameStateMachine>().AsSingle();
            
            Container.Bind<UnitFactory>().AsSingle();
            
            Container.BindInstance(humanSpawner);
            Container.BindInstance(zombieSpawner);
        }
    }
}