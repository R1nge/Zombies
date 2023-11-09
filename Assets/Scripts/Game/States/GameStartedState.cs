using Game.Services;
using Units;
using UnityEngine;

namespace Game.States
{
    public class GameStartedState : IGameState
    {
        private readonly UnitRTSController _unitRtsController;
        private readonly CameraService _cameraService;

        public GameStartedState(UnitRTSController unitRtsController, CameraService cameraService)
        {
            _unitRtsController = unitRtsController;
            _cameraService = cameraService;
        }

        public void Enter()
        {
            Debug.LogError("ENTERED GAME STARTED STATE");
            _unitRtsController.SelectFirst();
            _cameraService.LookAtSelectedUnit();
        }

        public void Exit()
        {
            throw new System.NotImplementedException();
        }
    }
}