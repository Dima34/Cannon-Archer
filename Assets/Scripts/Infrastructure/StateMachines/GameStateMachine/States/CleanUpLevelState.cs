using Zenject;

namespace Infrastructure.StateMachines.GameStateMachine.States
{
    public class CleanUpLevelState : IGameState
    {
        private readonly IGameStateMachine _gameStateMachine;

        public CleanUpLevelState(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }
        
        public void Enter()
        {
            CleanupLevel();
            GoToInitializeState();
        }

        private void CleanupLevel()
        {
            
        }

        private void GoToInitializeState() =>
            _gameStateMachine.EnterState<InitializeState>();

        public void Exit()
        {
        }

        public class Factory : PlaceholderFactory<IGameStateMachine, CleanUpLevelState>
        {
        }
    }
} 