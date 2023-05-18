using _Project._Codebase.Services.Input;
using Zenject;

namespace _Project._Codebase.CompositionRoot
{
    public class InputInstaller : Installer<InputInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<Controls>().AsSingle();
            Container.BindInterfacesTo<InputService>().AsSingle();
        }
    }
}