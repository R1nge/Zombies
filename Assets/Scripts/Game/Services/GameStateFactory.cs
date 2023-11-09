using Game.States;
using Units;

namespace Game.Services
{
    public class GameStateFactory
    {
        private readonly CoroutineRunner _coroutineRunner;
        private readonly UnitRTSController _unitRtsController;
        private readonly CameraService _cameraService;
        
        private GameStateFactory(CoroutineRunner coroutineRunner, UnitRTSController unitRtsController, CameraService cameraService)
        {
            _coroutineRunner = coroutineRunner;
            _unitRtsController = unitRtsController;
            _cameraService = cameraService;
        }

        public IGameState CreateGameInitState(GameStateMachine gameStateMachine)
        {
            return new GameInitState(gameStateMachine, _coroutineRunner);
        }

        public IGameState CreateGameStartedState(GameStateMachine gameStateMachine)
        {
            return new GameStartedState(_unitRtsController, _cameraService);
        }
    }
}