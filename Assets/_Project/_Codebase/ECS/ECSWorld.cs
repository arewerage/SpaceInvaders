﻿using System;
using _Project._Codebase.ECS.Assets;
using _Project._Codebase.ECS.Common;
using _Project._Codebase.ECS.Enemy;
using _Project._Codebase.ECS.Game;
using _Project._Codebase.ECS.Input;
using _Project._Codebase.ECS.Laser;
using _Project._Codebase.ECS.Physics;
using _Project._Codebase.ECS.Pickable;
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
            
            BindInitializer<AssetProviderSystem>();
            
            BindSystem<GameplayInitializeSystem>();
            
            BindSystem<PlayerSpawnSystem>();
            BindSystem<PlayerVelocitySystem>();
            BindSystem<PlayerShootingSystem>();
            BindSystem<PlayerHitLaserSystem>();
            BindSystem<PlayerPickupLaserSystem>();
            BindSystem<PlayerScoreSystem>();
            
            BindSystem<EnemyWaveSpawnSystem>();
            BindSystem<EnemySpawnSystem>();
            BindSystem<EnemyHitLaserSystem>();
            
            BindSystem<LaserSpawnSystem>();
            BindSystem<PickableLaserSpawnSystem>();
            
            BindSystem<GameplayInputSystem>();
            
            BindSystem<PhysicsMovementSystem>();
            BindSystem<PhysicsCollisionSystem>();
            
            BindSystem<OffscreenCheckSystem>();
            BindSystem<DestroyEntitySystem>();
            
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