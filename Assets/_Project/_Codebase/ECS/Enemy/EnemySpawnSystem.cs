using _Project._Codebase.ECS.Assets;
using _Project._Codebase.ECS.Common;
using _Project._Codebase.ECS.Extensions;
using _Project._Codebase.ECS.Player;
using _Project._Codebase.ECS.UnityRelated;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace _Project._Codebase.ECS.Enemy
{
    using Scellecs.Morpeh;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class EnemySpawnSystem : ISystem
    {
        private Event<EnemySpawnRequest> _enemySpawnRequest;
        private Stash<AssetProviderComponent> _assetProviderStash;
        private Filter _assetProvider;

        public World World { get; set; }

        public void OnAwake()
        {
            _enemySpawnRequest = World.GetEvent<EnemySpawnRequest>();
            _assetProviderStash = World.GetStash<AssetProviderComponent>();
            _assetProvider = World.Filter.With<AssetProviderComponent>().With<PlayerMarker>();
        }
        
        public void OnUpdate(float deltaTime)
        {
            if (_enemySpawnRequest.IsPublished == false)
                return;

            foreach (EnemySpawnRequest enemySpawnRequest in _enemySpawnRequest.BatchedChanges)
            foreach (Entity asset in _assetProvider)
            {
                ref var assetProvider = ref _assetProviderStash.Get(asset);

                SpawnEnemy(assetProvider.Result.Object, enemySpawnRequest);
            }
        }

        public void Dispose()
        {
        }

        private void SpawnEnemy(GameObject gameObject, EnemySpawnRequest enemySpawnRequest)
        {
            GameObject enemyObject = Object.Instantiate(gameObject);
            
            if (enemyObject.TryGetEntity(out Entity enemyEntity) == false)
                return;
            
            enemyEntity.SetComponent(new EnemyMarker());
            enemyEntity.SetComponent(new VelocityComponent { Value = Vector2.zero });
            enemyEntity.SetComponent(new LaserWeaponTypeComponent { LaserId = enemySpawnRequest.EnemyType.LaserId });

            var rigidbody = enemyEntity.GetComponent<Rigidbody2dComponent>().Value;
            rigidbody.position = enemySpawnRequest.Position;
            rigidbody.rotation = 180f;
            
            var renderer = enemyEntity.GetComponent<SpriteRendererComponent>().Value;
            renderer.sprite = enemySpawnRequest.EnemyType.Sprite;
        }
    }
}