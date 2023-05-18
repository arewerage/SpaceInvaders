using _Project._Codebase.Core.StateMachine;
using _Project._Codebase.Services.Game;
using _Project._Codebase.Services.Game.States;
using Zenject;

namespace _Project._Codebase.CompositionRoot.Gameplay
{
    public class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            InstallStateInjectorBindings();
        }
        
        private void InstallStateInjectorBindings()
        {
            Container.BindInterfacesAndSelfTo<InitGameplayState>().AsSingle();
            
            Container
                .Bind<BaseState>()
                .To(typeof(InitGameplayState))
                .FromResolveAll()
                .WhenInjectedInto<SceneStateInjector<BaseState, Game>>();

            Container.BindInterfacesTo<SceneStateInjector<BaseState, Game>>().AsSingle();
        }
    }
}