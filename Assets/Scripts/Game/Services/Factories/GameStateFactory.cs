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
        private readonly MilitarySpawner _militarySpawner;

        private GameStateFactory(CoroutineRunner coroutineRunner, UnitRTSController unitRtsController, CameraService cameraService, UIFactory uiFactory, ZombieSpawner zombieSpawner, MilitarySpawner militarySpawner)
        {
            _coroutineRunner = coroutineRunner;
            _unitRtsController = unitRtsController;
            _cameraService = cameraService;
            _uiFactory = uiFactory;
            _zombieSpawner = zombieSpawner;
            _militarySpawner = militarySpawner;
        }

        public IGameState CreateGameInitState(GameStateMachine gameStateMachine)
        {
            return new GameInitState(gameStateMachine, _coroutineRunner, _zombieSpawner, _militarySpawner, _cameraService);
        }

        public IGameState CreateGameStartedState(GameStateMachine gameStateMachine)
        {
            return new GameStartedState( _uiFactory, _unitRtsController, _cameraService);
        }
    }
}