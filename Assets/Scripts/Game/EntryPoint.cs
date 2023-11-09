using UnityEngine;
using Zenject;

namespace Game
{
    public class EntryPoint : MonoBehaviour
    {
        private GameStateMachine _gameStateMachine;
        
        [Inject]
        private void Inject(GameStateMachine gameStateMachine) => _gameStateMachine = gameStateMachine;

        private void Start() => _gameStateMachine.SwitchState(GameStateMachine.GameStates.Init);
    }
}