using _Project._Codebase.Core.Assets;
using _Project._Codebase.Core.StateMachine;
using _Project._Codebase.ECS._OLD.Laser;
using _Project._Codebase.ECS._OLD.Player;
using _Project._Codebase.Services.Game;
using _Project._Codebase.Services.Game.States;
using UnityEngine;
using Zenject;

namespace _Project._Codebase.CompositionRoot.Gameplay
{
    public class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            FactoryInstaller.Install(Container);

            Container.BindInterfacesTo<AssetFactory<MonoBehaviour, LaserMarkerProvider>>()
                .AsSingle().WhenInjectedInto<LaserSpawnSystem>();
            
            Container.BindInterfacesTo<AssetFactory<MonoBehaviour, PlayerMarkerProvider>>()
                .AsSingle().WhenInjectedInto<PlayerInitSystem>();
            
            ECSInstaller.Install(Container);
            
            InstallStateInjectorBindings();
        }
        
        private void InstallStateInjectorBindings()
        {
            Container.BindInterfacesAndSelfTo<InitLevelState>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameLoopState>().AsSingle();
            
            Container
                .Bind<BaseState>()
                .To(typeof(InitLevelState), typeof(GameLoopState))
                .FromResolveAll()
                .WhenInjectedInto<SceneStateInjector<BaseState, Game>>();

            Container.BindInterfacesTo<SceneStateInjector<BaseState, Game>>().AsSingle();
        }
    }
}