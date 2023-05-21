using System;
using _Project._Codebase.ECS.Common;
using _Project._Codebase.ECS.Enemy;
using _Project._Codebase.ECS.Input;
using _Project._Codebase.ECS.Laser;
using _Project._Codebase.ECS.Physics;
using _Project._Codebase.ECS.Player;
using Scellecs.Morpeh;
using Zenject;

namespace _Project._Codebase.ECS
{
    public class ECSWorld : IInitializable, IDisposable
    {
        private readonly DiContainer _container;
        private readonly World _world;

        private SystemsGroup _systemsGroup;

        public World World => _world;

        public ECSWorld(DiContainer container, World world)
        {
            _container = container;
            _world = world;
        }
        
        public void Initialize()
        {
            _world.UpdateByUnity = false;
            _systemsGroup = _world.CreateSystemsGroup();
            
            BindInitializer<PlayerInitSystem>();
            BindInitializer<EnemySpawnSystem>();
            
            BindSystem<GameplayInputSystem>();
            BindSystem<PlayerVelocitySystem>();
            BindSystem<PlayerShootingSystem>();
            BindSystem<PlayerHitLaserSystem>();
            BindSystem<PlayerDestroySystem>();
            BindSystem<EnemyHitLaserSystem>();
            BindSystem<EnemyDestroySystem>();
            BindSystem<LaserSpawnSystem>();
            BindSystem<PhysicsVelocitySystem>();
            BindSystem<DestroyingOnOffscreenSystem>();
            
            _world.AddSystemsGroup(0, _systemsGroup);
        }

        public void Dispose()
        {
            _systemsGroup.Dispose();
            _world.RemoveSystemsGroup(_systemsGroup);
        }

        private void BindInitializer<T>() where T : class, IInitializer
        {
            _systemsGroup.AddInitializer(_container.Instantiate<T>());
        }
        
        private void BindSystem<T>() where T : class, ISystem
        {
            _systemsGroup.AddSystem(_container.Instantiate<T>());
        }
    }
}