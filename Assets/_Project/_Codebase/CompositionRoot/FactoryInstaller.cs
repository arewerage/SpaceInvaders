using _Project._Codebase.Core.Assets;
using _Project._Codebase.Services.UI.Screen;
using _Project._Codebase.Services.UI.UIRootProvider;
using _Project._Codebase.UI;
using _Project._Codebase.UI.Factory;
using _Project._Codebase.UI.Screens;
using Zenject;

namespace _Project._Codebase.CompositionRoot
{
    public class FactoryInstaller : Installer<FactoryInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<AssetFactory<UIRoot, UIRoot>>()
                .AsSingle()
                .WhenInjectedInto<UIRootProvider>();
            Container.BindInterfacesTo<UIRootProvider>().AsSingle();

            Container.BindInterfacesTo<ScreenFactory<IUIScreen, BaseUIScreen>>()
                .AsSingle()
                .WhenInjectedInto<UIScreenService>();
            Container.BindInterfacesTo<UIScreenService>().AsSingle();
        }
    }
}