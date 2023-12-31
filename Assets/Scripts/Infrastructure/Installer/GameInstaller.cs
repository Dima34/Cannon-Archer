using Infrastructure.Factories;
using Infrastructure.Factories.UI;
using Infrastructure.Services.CoroutineRunner;
using Infrastructure.Services.Input;
using Infrastructure.Services.TextureDrawService;
using Infrastructure.StateMachines.GameStateMachine;
using Infrastructure.StateMachines.GameStateMachine.States;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installer
{
    public class GameInstaller : MonoInstaller, IInitializable
    {
        public override void InstallBindings()
        {
            BindSelfAsInitializable();
            BindCoroutineRunner();
            BindInputService();
            BindGameFactory();
            BindUIFactory();
            BindTextureDrawService();
            BindStateMachineStatesFactory();
            BindGameStateMachine();
        }

        private void BindCoroutineRunner() =>
            Container
                .BindInterfacesAndSelfTo<CoroutineRunnerService>()
                .FromNewComponentOn(gameObject)
                .AsSingle();

        private void BindSelfAsInitializable() =>
            Container
                .BindInterfacesAndSelfTo<GameInstaller>()
                .FromInstance(this)
                .AsSingle();

        private void BindInputService()
        {
            IInputService inputService;

            if (Application.isEditor)
                inputService = new StandaloneInputService();
            else
                inputService = new MobileInputService();

            Container
                .Bind<IInputService>()
                .FromInstance(inputService)
                .AsSingle();
        }

        private void BindGameFactory() =>
            Container
                .Bind<IGameFactory>()
                .To<GameFactory>()
                .AsSingle();

        private void BindUIFactory() =>
            Container
                .Bind<IUIFactory>()
                .To<UIFactory>()
                .AsSingle();

        private void BindTextureDrawService()
        {
            Container
                .Bind<ITextureDrawService>()
                .To<TextureDrawService>()
                .AsSingle();
        }

        private void BindStateMachineStatesFactory()
        {
            BindInitializeStateFactory();
            BindGameIdleStateFactory();
            BindGameLoopStateFactory();
            BindEndgameStateFactory();
            BindCleanupLevelStateFactory();
        }

        private void BindInitializeStateFactory() =>
            Container
                .BindFactory<IGameStateMachine,InitializeState, InitializeState.Factory>();

        private void BindGameIdleStateFactory() =>
            Container
                .BindFactory<IGameStateMachine,GameIdleStateState, GameIdleStateState.Factory>();

        private void BindCleanupLevelStateFactory() =>
            Container
                .BindFactory<IGameStateMachine, CleanUpLevelState, CleanUpLevelState.Factory>();

        private void BindEndgameStateFactory() =>
            Container
                .BindFactory<IGameStateMachine, EndGameState, EndGameState.Factory>();

        private void BindGameLoopStateFactory() =>
            Container
                .BindFactory<IGameStateMachine,GameLoopState, GameLoopState.Factory>();

        private void BindGameStateMachine() =>
            Container
                .BindInterfacesAndSelfTo<GameStateMachine>()
                .AsSingle();

        public void Initialize() =>
            GetStateMachineAndEnterInitialState();

        private void GetStateMachineAndEnterInitialState()
        {
            GameStateMachine gameStateMachine = Container.Resolve<GameStateMachine>();
            gameStateMachine.EnterState<InitializeState>();
        }
    }
}