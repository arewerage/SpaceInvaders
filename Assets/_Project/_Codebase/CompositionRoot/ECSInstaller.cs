using _Project._Codebase.ECS;
using Scellecs.Morpeh;
using Zenject;

namespace _Project._Codebase.CompositionRoot
{
    public class ECSInstaller : Installer<ECSInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInstance(World.Default).AsSingle();
            Container.Bind<ECSWorld>().AsSingle();
        }
    }
}