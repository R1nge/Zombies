using Game.Services;
using Game.Services.Factories;

namespace Game.States
{
    public class GameInitState : IGameState
    {
        private readonly UIFactory _uiFactory;
        private readonly ZombieSpawner _zombieSpawner;
        private readonly MilitarySpawner _militarySpawner;
        private readonly CameraService _cameraService;

        public GameInitState(UIFactory uiFactory, ZombieSpawner zombieSpawner, MilitarySpawner militarySpawner, CameraService cameraService)
        {
            _uiFactory = uiFactory;
            _zombieSpawner = zombieSpawner;
            _militarySpawner = militarySpawner;
            _cameraService = cameraService;
        }

        public void Enter()
        {
            _uiFactory.CreateInGameUI();
            _militarySpawner.Spawn();
            _zombieSpawner.Spawn();
            _cameraService.FlyThrough();
        }
        
        public void Exit() { }
    }
}