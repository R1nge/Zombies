using Game.Services.Factories;

namespace Game.States
{
    public class GameLostState : IGameState
    {
        private readonly UIFactory _uiFactory;

        public GameLostState(UIFactory uiFactory) => _uiFactory = uiFactory;

        public void Enter() => _uiFactory.CreateLostUI();

        public void Exit() { }
    }
}