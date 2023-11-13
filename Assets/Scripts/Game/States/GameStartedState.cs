using Game.Services;
using Game.Services.Factories;
using Units;

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
            _uiFactory.CreateInGameUI();
            _unitRtsController.SelectFirst();
            _cameraService.LookAtSelectedUnit();
        }

        public void Exit() { }
    }
}