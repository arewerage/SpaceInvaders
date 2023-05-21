using _Project._Codebase.ECS.Assets;
using _Project._Codebase.ECS.Enemy;
using _Project._Codebase.ECS.UnityRelated;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace _Project._Codebase.ECS.Common
{
    using Scellecs.Morpeh;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class DestroyEntitySystem : ISystem
    {
        private Event<EnemyWaveSpawnRequest> _enemyWaveSpawnRequest;
        private Stash<Rigidbody2dComponent> _rigidbodyStash;
        private Filter _entities;
        private Filter _enemies;
        
        public World World { get; set; }

        public void OnAwake()
        {
            _enemyWaveSpawnRequest = World.GetEvent<EnemyWaveSpawnRequest>();
            _rigidbodyStash = World.GetStash<Rigidbody2dComponent>();
            _entities = World.Filter.With<DestroyComponent>().With<Rigidbody2dComponent>();
            _enemies = World.Filter.With<EnemyMarker>().Without<AssetProviderComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity entity in _entities)
            {
                ref var rigidbody = ref _rigidbodyStash.Get(entity);
                
                Object.Destroy(rigidbody.Value.gameObject);
                
                if (_enemies.IsEmpty())
                    _enemyWaveSpawnRequest.NextFrame(new EnemyWaveSpawnRequest());
            }
        }

        public void Dispose()
        {
        }
    }
}