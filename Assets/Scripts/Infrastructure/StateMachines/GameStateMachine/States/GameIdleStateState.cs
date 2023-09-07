using Infrastructure.Factories.UI;
using Zenject;

namespace Infrastructure.StateMachines.GameStateMachine.States
{
    public class GameIdleStateState : IGameState
    {
        private IGameStateMachine _gameStateMachine;

        public GameIdleStateState(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            EnterGameLoopState();
        }

        private void EnterGameLoopState() =>
            _gameStateMachine.EnterState<GameLoopState>();

        public void Exit()
        {
            
        }

        public class Factory : PlaceholderFactory<IGameStateMachine, GameIdleStateState>
        {
        }
    }
}