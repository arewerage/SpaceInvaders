using _Project._Codebase.Core.Assets;
using _Project._Codebase.Core.StateMachine;
using _Project._Codebase.Services.Game;
using _Project._Codebase.Services.Game.States;
using _Project._Codebase.Services.UI.Screen;
using _Project._Codebase.Services.UI.UIRootProvider;
using _Project._Codebase.UI;
using _Project._Codebase.UI.Factory;
using _Project._Codebase.UI.Screens;
using Zenject;

namespace _Project._Codebase.CompositionRoot.MainMenu
{
    public class MainMenuInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<AssetFactory<UIRoot, UIRoot>>()
                .AsSingle()
                .WhenInjectedInto<UIRootProvider>();
            Container.BindInterfacesTo<UIRootProvider>().AsSingle();

            Container.BindInterfacesTo<ScreenFactory<IUIScreen, BaseUIScreen>>()
                .AsSingle()
                .WhenInjectedInto<ScreenService>();
            Container.BindInterfacesTo<ScreenService>().AsSingle();
            
            InstallStateInjectorBindings();
        }

        private void InstallStateInjectorBindings()
        {
            Container.BindInterfacesAndSelfTo<InitMainMenuState>().AsSingle();
            Container.BindInterfacesAndSelfTo<LoadGameplayState>().AsSingle();
            
            Container
                .Bind<BaseState>()
                .To(typeof(InitMainMenuState), typeof(LoadGameplayState))
                .FromResolveAll()
                .WhenInjectedInto<SceneStateInjector<BaseState, Game>>();

            Container.BindInterfacesTo<SceneStateInjector<BaseState, Game>>().AsSingle();
        }
    }
}