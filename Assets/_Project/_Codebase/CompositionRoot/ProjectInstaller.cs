using _Project._Codebase.Configs;
using _Project._Codebase.Core.Assets.Provider;
using _Project._Codebase.Core.SceneLoader;
using _Project._Codebase.Core.StateMachine;
using _Project._Codebase.Services.Game;
using _Project._Codebase.Services.Game.States;
using UnityEngine;
using Zenject;

namespace _Project._Codebase.CompositionRoot
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private ScreenConfig _screenConfig;
        
        public override void InstallBindings()
        {
            InputInstaller.Install(Container);
            Container.BindInstance(_screenConfig);
            Container.BindInterfacesTo<AssetProvider>().AsSingle();
            Container.BindInterfacesTo<SceneLoader>().AsSingle();
            
            InstallStateMachineBindings();

            Container.BindInterfacesAndSelfTo<Game>().AsSingle();
        }

        private void InstallStateMachineBindings()
        {
            Container.BindInterfacesAndSelfTo<BootstrapState>().AsSingle();
            Container.BindInterfacesAndSelfTo<LoadMainMenuState>().AsSingle();

            Container
                .Bind<BaseState>()
                .To(typeof(BootstrapState), typeof(LoadMainMenuState))
                .FromResolveAll()
                .WhenInjectedInto<StateMachine<BaseState, Game>>();

            Container.BindInterfacesTo<StateMachine<BaseState, Game>>().AsSingle();
        }
    }
}