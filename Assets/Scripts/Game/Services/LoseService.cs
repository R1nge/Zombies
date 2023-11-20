using System;
using Units;
using UnityEngine;
using Zenject;

namespace Game.Services
{
    public class LoseService : IInitializable, IDisposable
    {
        private readonly UnitRTSController _unitRtsController;
        private readonly GameStateMachine _gameStateMachine;

        private LoseService(UnitRTSController unitRtsController, GameStateMachine gameStateMachine)
        {
            _unitRtsController = unitRtsController;
            _gameStateMachine = gameStateMachine;
        }

        public void Initialize() => _unitRtsController.OnZombiesAmountChanged += ZombieChanged;

        private void ZombieChanged(int previous, int amount)
        {
            Debug.Log($"Available Units {_unitRtsController.AvailableUnitsCount}");
            Debug.Log($"Pending Units {_unitRtsController.PendingUnitsCount}");
            if (_unitRtsController.AvailableUnitsCount == 0 && _unitRtsController.PendingUnitsCount == 0)
            {
                _gameStateMachine.SwitchState(GameStateMachine.GameStates.Lose);
            }
        }

        public void Dispose() => _unitRtsController.OnZombiesAmountChanged -= ZombieChanged;
    }
}