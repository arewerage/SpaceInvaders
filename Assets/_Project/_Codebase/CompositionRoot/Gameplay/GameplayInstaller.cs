using _Project._Codebase.Configs;
using _Project._Codebase.Configs.Enemy;
using _Project._Codebase.Core.Assets;
using _Project._Codebase.Core.StateMachine;
using _Project._Codebase.Services.Game;
using _Project._Codebase.Services.Game.States;
using Scellecs.Morpeh.Providers;
using UnityEngine;
using Zenject;

namespace _Project._Codebase.CompositionRoot.Gameplay
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private EnemyConfig _enemyConfig;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_gameConfig).AsSingle();
            Container.BindInstance(_playerConfig).AsSingle();
            Container.BindInstance(_enemyConfig).AsSingle();
            
            Container.BindInterfacesTo<AssetFactory<MonoBehaviour, RemoveEntityOnDestroy>>().AsSingle();
            
            FactoryInstaller.Install(Container);
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