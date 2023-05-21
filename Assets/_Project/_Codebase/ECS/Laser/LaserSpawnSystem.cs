using System.Collections.Generic;
using System.Linq;
using _Project._Codebase.Configs.Laser;
using _Project._Codebase.ECS.Assets;
using _Project._Codebase.ECS.Common;
using _Project._Codebase.ECS.Extensions;
using _Project._Codebase.ECS.UnityRelated;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace _Project._Codebase.ECS.Laser
{
    using Scellecs.Morpeh;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class LaserSpawnSystem : ISystem
    {
        private readonly Dictionary<LaserId, LaserType> _laserConfig;

        private Event<LaserSpawnRequest> _laserSpawnRequest;
        private Stash<AssetProviderComponent> _assetProviderStash;
        private Filter _assetProvider;

        public World World { get; set; }

        public LaserSpawnSystem(LaserConfig laserConfig)
        {
            _laserConfig = laserConfig.LaserList.ToDictionary(x => x.LaserId, x => x);
        }

        public void OnAwake()
        {
            _laserSpawnRequest = World.GetEvent<LaserSpawnRequest>();
            _assetProviderStash = World.GetStash<AssetProviderComponent>();
            _assetProvider = World.Filter.With<AssetProviderComponent>().With<LaserMarker>();
        }

        public void OnUpdate(float deltaTime)
        {
            if (_laserSpawnRequest.IsPublished == false)
                return;

            foreach (LaserSpawnRequest laserSpawnRequest in _laserSpawnRequest.BatchedChanges)
            foreach (Entity asset in _assetProvider)
            {
                ref var assetProvider = ref _assetProviderStash.Get(asset);

                SpawnLaser(assetProvider.Result.Object, laserSpawnRequest);
            }
        }

        public void Dispose()
        {
        }

        private void SpawnLaser(GameObject gameObject, LaserSpawnRequest laserSpawnRequest)
        {
            GameObject laserObject = Object.Instantiate(gameObject);
            
            if (laserObject.TryGetEntity(out Entity laserEntity) == false)
                return;
            
            laserEntity.SetComponent(new LaserMarker());
            laserEntity.SetComponent(new OwnerComponent { Value = laserSpawnRequest.Owner });
            laserEntity.SetComponent(new SpeedComponent { Value = _laserConfig[laserSpawnRequest.LaserId].Speed });
            laserEntity.SetComponent(new VelocityComponent { Value = Vector2.up });

            var rigidbody = laserEntity.GetComponent<Rigidbody2dComponent>().Value;
            rigidbody.position = laserSpawnRequest.Position;
            rigidbody.rotation = laserSpawnRequest.Angle;

            var renderer = laserEntity.GetComponent<SpriteRendererComponent>().Value;
            renderer.sprite = _laserConfig[laserSpawnRequest.LaserId].Sprite;
        }
    }
}