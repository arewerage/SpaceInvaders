using System.Collections.Generic;
using System.Linq;
using _Project._Codebase.Configs.Laser;
using _Project._Codebase.ECS.Assets;
using _Project._Codebase.ECS.Common;
using _Project._Codebase.ECS.Extensions;
using _Project._Codebase.ECS.UnityRelated;
using _Project._Codebase.Extensions;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace _Project._Codebase.ECS.Pickable
{
    using Scellecs.Morpeh;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class PickableLaserSpawnSystem : ISystem
    {
        private const float Speed = 1f;
        
        private readonly Dictionary<LaserId, LaserType> _laserConfig;
        
        private Event<PickableLaserSpawnRequest> _pickableLaserSpawnRequest;
        private Stash<AssetProviderComponent> _assetProviderStash;
        private Filter _assetProvider;

        public World World { get; set; }

        public PickableLaserSpawnSystem(LaserConfig laserConfig)
        {
            _laserConfig = laserConfig.LaserList.ToDictionary(x => x.LaserId, x => x);
        }

        public void OnAwake()
        {
            _pickableLaserSpawnRequest = World.GetEvent<PickableLaserSpawnRequest>();
            _assetProviderStash = World.GetStash<AssetProviderComponent>();
            _assetProvider = World.Filter.With<AssetProviderComponent>().With<PickableMarker>();
        }

        public void OnUpdate(float deltaTime)
        {
            if (_pickableLaserSpawnRequest.IsPublished == false)
                return;

            foreach (PickableLaserSpawnRequest pickableLaserSpawnRequest in _pickableLaserSpawnRequest.BatchedChanges)
            foreach (Entity asset in _assetProvider)
            {
                ref var assetProvider = ref _assetProviderStash.Get(asset);

                SpawnPickable(assetProvider.Result.Object, pickableLaserSpawnRequest.Position);
            }
        }

        public void Dispose()
        {
        }

        private void SpawnPickable(GameObject gameObject, Vector2 position)
        {
            GameObject laserObject = Object.Instantiate(gameObject);
            
            if (laserObject.TryGetEntity(out Entity laserEntity) == false)
                return;

            LaserId laserId = EnumExtensions.GetRandom<LaserId>();
            
            laserEntity.SetComponent(new PickableMarker());
            laserEntity.SetComponent(new VelocityComponent { Value = Vector2.down });
            laserEntity.SetComponent(new SpeedComponent { Value = Speed });
            laserEntity.SetComponent(new LaserWeaponTypeComponent { LaserId = laserId });

            var rigidbody = laserEntity.GetComponent<Rigidbody2dComponent>().Value;
            rigidbody.position = position;
            rigidbody.rotation = 180f;
            
            var renderer = laserEntity.GetComponent<SpriteRendererComponent>().Value;
            renderer.sprite = _laserConfig[laserId].PickableSprite;
        }
    }
}