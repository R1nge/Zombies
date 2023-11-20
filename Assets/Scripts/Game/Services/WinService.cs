using Units;
using Zenject;

namespace Game.Services
{
    public class WinService : IInitializable
    {
        private readonly UnitRTSController _unitRtsController;
        private readonly GameStateMachine _gameStateMachine;

        private WinService(UnitRTSController unitRtsController, GameStateMachine gameStateMachine)
        {
            _unitRtsController = unitRtsController;
            _gameStateMachine = gameStateMachine;
        }

        public void Initialize()
        {
            _unitRtsController.OnZombiesAmountChanged += ZombieChanged;
            _unitRtsController.OnPendingUnitsAmountChanged += PendingChanged;
        }

        private void ZombieChanged(int previous, int amount)
        {
            if (_unitRtsController.AvailableUnitsCount == 0 && _unitRtsController.PendingUnitsCount == 0)
            {
                _gameStateMachine.SwitchState(GameStateMachine.GameStates.Lose);
            }
        }

        private void PendingChanged(int previous, int amount)
        {
            if (_unitRtsController.AvailableUnitsCount == 0 && _unitRtsController.PendingUnitsCount == 0)
            {
                _gameStateMachine.SwitchState(GameStateMachine.GameStates.Lose);
            }
        }
    }
}