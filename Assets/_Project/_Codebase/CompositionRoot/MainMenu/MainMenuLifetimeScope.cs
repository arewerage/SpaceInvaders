using _Project._Codebase.Core.StateMachine;
using _Project._Codebase.Services.Game;
using _Project._Codebase.Services.Game.States;
using VContainer;
using VContainer.Unity;

namespace _Project._Codebase.CompositionRoot.MainMenu
{
    public class MainMenuLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            ConfigureSceneStateInjector(builder);
        }
        
        private static void ConfigureSceneStateInjector(IContainerBuilder builder)
        {
            builder.Register<BaseState, InitMainMenuState>(Lifetime.Singleton).AsSelf();
            builder.Register(container =>
            {
                IManageableStateMachine<BaseState, Game> manageableStateMachine = container.Resolve<IManageableStateMachine<BaseState, Game>>();
                InitMainMenuState initMainMenuState = container.Resolve<InitMainMenuState>();
                BaseState[] states = { initMainMenuState };

                return new SceneStateInjector<BaseState, Game>(manageableStateMachine, states);
            }, Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
        }
    }
}