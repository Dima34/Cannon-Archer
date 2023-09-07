using Infrastructure.Factories;
using Infrastructure.Factories.UI;
using Infrastructure.Services.StaticData;
using StaticData;
using UnityEngine;
using Zenject;

namespace Infrastructure.StateMachines.GameStateMachine.States
{
    public class InitializeState : IGameState
    {
        private IGameStateMachine _stateMachine;
        private IGameFactory _gameFactory;
        private LevelStaticData _levelData;
        private IUIFactory _uiFactory;

        public InitializeState(IGameStateMachine stateMachine, IGameFactory gameFactory, IUIFactory uiFactory, IStaticDataService staticDataService)
        {
            _levelData = staticDataService.GetCurrentLevelData();
            _uiFactory = uiFactory;
            _stateMachine = stateMachine;
            _gameFactory = gameFactory;
        }
        
        public void Enter()
        {
            BuildLevel();
            EnterGameIdleState();
        }

        private void BuildLevel()
        {
            CreateAndPlacePlayer();
            CreateAndPlaceMap();
            CreateHUD();
        }

        private void CreateAndPlacePlayer()
        {
            GameObject player = _gameFactory.CreatePlayer();
            player.transform.position = _levelData.PlayerStartPosition;
        }

        private void CreateAndPlaceMap()
        {
            GameObject map = _gameFactory.CreateMap();
            map.transform.position = _levelData.LevelSpawnPosition;
        }

        private void CreateHUD() =>
            _uiFactory.CreateHUD();

        private void EnterGameIdleState() =>
            _stateMachine.EnterState<GameIdleStateState>();

        public void Exit()
        {
        }

        public class Factory : PlaceholderFactory<IGameStateMachine, InitializeState>
        {
        }
    }
}