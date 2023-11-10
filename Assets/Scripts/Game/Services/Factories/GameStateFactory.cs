using Game.States;
using Units;

namespace Game.Services.Factories
{
    public class GameStateFactory
    {
        private readonly CoroutineRunner _coroutineRunner;
        private readonly UnitRTSController _unitRtsController;
        private readonly CameraService _cameraService;
        private readonly UIFactory _uiFactory;
        private readonly ZombieSpawner _zombieSpawner;
        private readonly MilitarySpawner militarySpawner;

        private GameStateFactory(CoroutineRunner coroutineRunner, UnitRTSController unitRtsController, CameraService cameraService, UIFactory uiFactory, ZombieSpawner zombieSpawner, MilitarySpawner militarySpawner)
        {
            _coroutineRunner = coroutineRunner;
            _unitRtsController = unitRtsController;
            _cameraService = cameraService;
            _uiFactory = uiFactory;
            _zombieSpawner = zombieSpawner;
            this.militarySpawner = militarySpawner;
        }

        public IGameState CreateGameInitState(GameStateMachine gameStateMachine)
        {
            return new GameInitState(_uiFactory, _zombieSpawner, militarySpawner, _cameraService);
        }

        public IGameState CreateGameStartedState(GameStateMachine gameStateMachine)
        {
            return new GameStartedState(_unitRtsController, _cameraService);
        }
    }
}