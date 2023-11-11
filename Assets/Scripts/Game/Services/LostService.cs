using System;
using Units;
using UnityEngine;
using Zenject;

namespace Game.Services
{
    public class LostService : IInitializable, IDisposable
    {
        private readonly UnitRTSController _rtsController;
        private readonly GameStateMachine _gameStateMachine;

        private LostService(UnitRTSController unitRtsController, GameStateMachine gameStateMachine)
        {
            _rtsController = unitRtsController;
            _gameStateMachine = gameStateMachine;
        }


        public void Initialize()
        {
            Debug.LogError("INITAIZIZE");
            _rtsController.OnZombiesAmountChanged += ZombiesAmountChanged;
        }

        private void ZombiesAmountChanged(int previous, int current)
        {
            Debug.LogError(current);
            if (current == 0)
            {
                _gameStateMachine.SwitchState(GameStateMachine.GameStates.Lose);
            }
        }

        public void Dispose() => _rtsController.OnZombiesAmountChanged -= ZombiesAmountChanged;
    }
}