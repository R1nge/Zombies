namespace Game.Services
{
    public class ZombieCounter
    {
        private int _zombieCount;

        private readonly GameStateMachine _gameStateMachine;

        private ZombieCounter(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Add()
        {
            _zombieCount++;
        }

        public void Remove()
        {
            _zombieCount--;

            if (_zombieCount == 0)
            {
                _gameStateMachine.SwitchState(GameStateMachine.GameStates.Lose);
            }
        }
    }
}