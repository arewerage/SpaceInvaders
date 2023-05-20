using _Project._Codebase.Core.StateMachine;
using _Project._Codebase.Services.Game;
using _Project._Codebase.Services.Game.States;
using Zenject;

namespace _Project._Codebase.CompositionRoot.MainMenu
{
    public class MainMenuInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            FactoryInstaller.Install(Container);
            
            InstallStateInjectorBindings();
        }

        private void InstallStateInjectorBindings()
        {
            Container.BindInterfacesAndSelfTo<InitMainMenuState>().AsSingle();
            Container.BindInterfacesAndSelfTo<LoadLevelState>().AsSingle();
            
            Container
                .Bind<BaseState>()
                .To(typeof(InitMainMenuState), typeof(LoadLevelState))
                .FromResolveAll()
                .WhenInjectedInto<SceneStateInjector<BaseState, Game>>();

            Container.BindInterfacesTo<SceneStateInjector<BaseState, Game>>().AsSingle();
        }
    }
}