using Infrastructure.Factories.UI;
using Zenject;

namespace Infrastructure.StateMachines.GameStateMachine.States
{
    public class EndGameState : IGameState
    {
        private IUIFactory _uiFactory;
        private IGameStateMachine _gameStateMachine;

        public EndGameState(IUIFactory uiFactory, IGameStateMachine gameStateMachine)
        {
            _uiFactory = uiFactory;
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
        }

        public void Exit()
        {
            
        }

        public class Factory : PlaceholderFactory<IGameStateMachine, EndGameState>
        {
        }
    }
}