using System.Collections;
using Game.Services;
using Game.Services.Factories;

namespace Game.States
{
    public class GameInitState : IGameState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly CoroutineRunner _coroutineRunner;
        private readonly UIFactory _uiFactory;
        private readonly ZombieSpawner _zombieSpawner;
        private readonly HumanSpawner _humanSpawner;
        private readonly CameraService _cameraService;

        public GameInitState(GameStateMachine gameStateMachine, CoroutineRunner coroutineRunner, UIFactory uiFactory, ZombieSpawner zombieSpawner, HumanSpawner humanSpawner, CameraService cameraService)
        {
            _gameStateMachine = gameStateMachine;
            _coroutineRunner = coroutineRunner;
            _uiFactory = uiFactory;
            _zombieSpawner = zombieSpawner;
            _humanSpawner = humanSpawner;
            _cameraService = cameraService;
        }

        public void Enter() => _coroutineRunner.StartCoroutine(Wait_C());

        private IEnumerator Wait_C()
        {   
            _uiFactory.CreateInGameUI();
            _humanSpawner.Spawn();
            _zombieSpawner.Spawn();
            _cameraService.FlyThrough();
            yield return null;
            _gameStateMachine.SwitchState(GameStateMachine.GameStates.Start);
        }

        public void Exit() { }
    }
}