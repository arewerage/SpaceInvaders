using _Project._Codebase.Configs;
using _Project._Codebase.ECS.Assets;
using _Project._Codebase.ECS.Common;
using _Project._Codebase.ECS.Extensions;
using _Project._Codebase.ECS.Input;
using _Project._Codebase.ECS.UnityRelated;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace _Project._Codebase.ECS.Player
{
    using Scellecs.Morpeh;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class PlayerSpawnSystem : ISystem
    {
        private Event<PlayerSpawnRequest> _playerSpawnRequest;
        private Stash<AssetProviderComponent> _assetProviderStash;
        private Filter _assetProvider;

        public World World { get; set; }

        public void OnAwake()
        {
            _playerSpawnRequest = World.GetEvent<PlayerSpawnRequest>();
            _assetProviderStash = World.GetStash<AssetProviderComponent>();
            _assetProvider = World.Filter.With<AssetProviderComponent>().With<PlayerMarker>();
        }
        
        public void OnUpdate(float deltaTime)
        {
            if (_playerSpawnRequest.IsPublished == false)
                return;
            
            foreach (PlayerSpawnRequest playerSpawnRequest in _playerSpawnRequest.BatchedChanges)
            foreach (Entity asset in _assetProvider)
            {
                ref var assetProvider = ref _assetProviderStash.Get(asset);

                SpawnPlayer(assetProvider.Result.Object, playerSpawnRequest.PlayerConfig);
            }
        }

        public void Dispose()
        {
        }

        private void SpawnPlayer(GameObject gameObject, PlayerConfig playerConfig)
        {
            GameObject playerObject = Object.Instantiate(gameObject);

            if (playerObject.TryGetEntity(out Entity playerEntity) == false)
                return;
            
            playerEntity.SetComponent(new PlayerMarker());
            playerEntity.SetComponent(new GameplayInputComponent { MoveAxis = Vector2.zero, IsShooting = false });
            playerEntity.SetComponent(new SpeedComponent { Value = playerConfig.Speed });
            playerEntity.SetComponent(new VelocityComponent { Value = Vector2.zero });
            playerEntity.SetComponent(new LaserWeaponTypeComponent { LaserId = playerConfig.LaserId });
            
            var renderer = playerEntity.GetComponent<SpriteRendererComponent>().Value;
            renderer.sprite = playerConfig.Sprite;
        }
    }
}