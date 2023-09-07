 using Infrastructure.Factories;
using Zenject;

namespace Infrastructure.StateMachines.GameStateMachine.States
{
    public class GameLoopState : IGameState
    {
        private IGameFactory _gameFactory;
        private IGameStateMachine _gameStateMachine;

        public GameLoopState(IGameFactory gameFactory, IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
            _gameFactory = gameFactory;
        }

        public void Enter()
        {
        }
        
        public void Exit()
        {
        }

        public class Factory : PlaceholderFactory<IGameStateMachine, GameLoopState>
        {
        }
    }
}