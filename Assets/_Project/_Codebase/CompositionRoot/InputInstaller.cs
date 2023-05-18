using _Project._Codebase.Services.Input;
using VContainer;
using VContainer.Unity;

namespace _Project._Codebase.CompositionRoot
{
    public class InputInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.Register<Controls>(Lifetime.Singleton);
            builder.Register<InputService>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}