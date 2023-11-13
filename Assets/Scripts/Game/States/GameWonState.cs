using Game.Services.Factories;

namespace Game.States
{
    public class GameWonState : IGameState
    {
        private readonly UIFactory _uiFactory;

        public GameWonState(UIFactory uiFactory) => _uiFactory = uiFactory;

        public void Enter() => _uiFactory.CreateWonUI();

        public void Exit() { }
    }
}