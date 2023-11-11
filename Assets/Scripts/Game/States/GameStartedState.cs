using Game.Services;
using Game.Services.Factories;
using Units;
using UnityEngine;

namespace Game.States
{
    public class GameStartedState : IGameState
    {
        private readonly UIFactory _uiFactory;
        private readonly UnitRTSController _unitRtsController;
        private readonly CameraService _cameraService;

        public GameStartedState(UIFactory uiFactory, UnitRTSController unitRtsController, CameraService cameraService)
        {
            _uiFactory = uiFactory;
            _unitRtsController = unitRtsController;
            _cameraService = cameraService;
        }

        public void Enter()
        {
            Debug.LogError("ENTERED GAME STARTED STATE");
            _uiFactory.CreateInGameUI();
            _unitRtsController.SelectFirst();
            _cameraService.LookAtSelectedUnit();
        }

        public void Exit() { }
    }
}