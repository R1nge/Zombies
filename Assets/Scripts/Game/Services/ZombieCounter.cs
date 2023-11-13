namespace Game.Services
{
    public class ZombieCounter
    {
        private int _zombieCount;
        private int _pendingZombieCount;

        private readonly GameStateMachine _gameStateMachine;

        private ZombieCounter(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }
        
        public void AddZombie()
        {
            _zombieCount++;
        }

        public void AddPending()
        {
            _pendingZombieCount++;
        }

        public void RemovePending()
        {
            _pendingZombieCount--;
        }

        public void RemoveZombie()
        {
            _zombieCount--;

            if (_zombieCount == 0 && _pendingZombieCount == 0)
            {
                _gameStateMachine.SwitchState(GameStateMachine.GameStates.Lose);
            }
        }
    }
}