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
        [SerializeField] private MilitarySpawner militarySpawner;
        [SerializeField] private ZombieSpawner zombieSpawner;
        [SerializeField] private CameraService cameraService;
        
        public override void InstallBindings()
        {
            Container.BindInstance(cameraService);
            
            Container.Bind<MarkerPositionService>().AsSingle();
           

            Container.Bind<UnitRTSController>().AsSingle();

            Container.Bind<UIFactory>().AsSingle();
            
            
            Container.Bind<UnitFactory>().AsSingle();
            
            Container.BindInstance(militarySpawner);
            Container.BindInstance(zombieSpawner);
            
            //WIN (humanCounter), LOST (UnitRtsController)
            Container.Bind<GameStateFactory>().AsSingle();
            Container.Bind<GameStateMachine>().AsSingle();
            
            Container.Bind<HumanCounter>().AsSingle();

            Container.BindInterfacesTo<LostService>().AsSingle();
        }
    }
}