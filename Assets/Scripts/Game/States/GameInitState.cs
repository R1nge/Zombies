using System.Collections;
using Game.Services;
using UnityEngine;

namespace Game.States
{
    public class GameInitState : IGameState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly CoroutineRunner _coroutineRunner;

        public GameInitState(GameStateMachine gameStateMachine, CoroutineRunner coroutineRunner)
        {
            _gameStateMachine = gameStateMachine;
            _coroutineRunner = coroutineRunner;
        }

        public void Enter() => _coroutineRunner.StartCoroutine(Wait_C());

        private IEnumerator Wait_C()
        {
            yield return null;
            _gameStateMachine.SwitchState(GameStateMachine.GameStates.Start);
        }

        public void Exit() { }
    }
}