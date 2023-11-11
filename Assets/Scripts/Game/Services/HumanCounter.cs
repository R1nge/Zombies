using System;

namespace Game.Services
{
    public class HumanCounter
    {
        public event Action<int> OnHumanCountChanged;
        public int HumanCount => _humanCount;
        private int _humanCount;

        private readonly GameStateMachine _gameStateMachine;

        private HumanCounter(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Add()
        {
            _humanCount++;
            OnHumanCountChanged?.Invoke(_humanCount);
        }

        public void Remove()
        {
            _humanCount--;
            OnHumanCountChanged?.Invoke(_humanCount);

            if (_humanCount == 0)
            {
                _gameStateMachine.SwitchState(GameStateMachine.GameStates.Win);
            }
        }
    }
}