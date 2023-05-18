using _Project._Codebase.Core.AssetProvider;
using _Project._Codebase.Core.SceneLoader;
using _Project._Codebase.Core.StateMachine;
using _Project._Codebase.Services.Game;
using _Project._Codebase.Services.Game.States;
using VContainer;
using VContainer.Unity;

namespace _Project._Codebase.CompositionRoot
{
    public class ProjectLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<InputInstaller>(Lifetime.Singleton);
            
            builder.Register<AssetProvider>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<SceneLoader>(Lifetime.Singleton).AsImplementedInterfaces();
            
            ConfigureStateMachine(builder);
            
            builder.RegisterEntryPoint<Game>();
        }

        private static void ConfigureStateMachine(IContainerBuilder builder)
        {
            builder.Register<BaseState, BootstrapState>(Lifetime.Singleton).AsSelf();
            builder.Register<BaseState, LoadMainMenuState>(Lifetime.Singleton).AsSelf();

            builder.Register<StateMachine<BaseState, Game>>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}