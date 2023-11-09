using System.Collections.Generic;
using Game.Services;
using Game.States;

namespace Game
{
    public class GameStateMachine
    {
        private readonly Dictionary<GameStates, IGameState> _states;
        private IGameState _currentState;
        private readonly GameStateFactory _gameStateFactory;

        private GameStateMachine(GameStateFactory gameStateFactory)
        {
            _states = new Dictionary<GameStates, IGameState>
            {
                { GameStates.Init, gameStateFactory.CreateGameInitState(this) },
                { GameStates.Start, gameStateFactory.CreateGameStartedState(this) }
            };
        }

        public void SwitchState(GameStates newState)
        {
            _currentState?.Exit();
            _currentState = _states[newState];
            _currentState.Enter();
        }

        public enum GameStates
        {
            Init,
            Start,
            Win,
            Lose
        }
    }
}