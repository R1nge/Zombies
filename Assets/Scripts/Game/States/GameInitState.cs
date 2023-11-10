using System.Collections;
using Game.Services;
using Game.Services.Factories;

namespace Game.States
{
    public class GameInitState : IGameState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly CoroutineRunner _coroutineRunner;
        
        private readonly ZombieSpawner _zombieSpawner;
        private readonly MilitarySpawner _militarySpawner;
        private readonly CameraService _cameraService;

        public GameInitState(GameStateMachine gameStateMachine, CoroutineRunner coroutineRunner, ZombieSpawner zombieSpawner, MilitarySpawner militarySpawner, CameraService cameraService)
        {
            _gameStateMachine = gameStateMachine;
            _coroutineRunner = coroutineRunner;
            _zombieSpawner = zombieSpawner;
            _militarySpawner = militarySpawner;
            _cameraService = cameraService;
        }

        public void Enter()
        {
            _coroutineRunner.StartCoroutine(SwitchToInGameState());
        }

        private IEnumerator SwitchToInGameState()
        {
            _militarySpawner.Spawn();
            _zombieSpawner.Spawn();
            yield return _cameraService.FlyThrough();
            _gameStateMachine.SwitchState(GameStateMachine.GameStates.Start);
        }
        
        public void Exit() { }
    }
}