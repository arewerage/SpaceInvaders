using System.Collections.Generic;
using _Project._Codebase.Constants;
using _Project._Codebase.Core.Assets.Provider;
using _Project._Codebase.ECS.Enemy;
using _Project._Codebase.ECS.Game;
using _Project._Codebase.ECS.Laser;
using _Project._Codebase.ECS.Pickable;
using _Project._Codebase.ECS.Player;
using Cysharp.Threading.Tasks;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace _Project._Codebase.ECS.Assets
{
    using Scellecs.Morpeh;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class AssetProviderSystem : IInitializer
    {
        private readonly IAssetProvider _assetProvider;
        private readonly List<AsyncOperationHandle> _handles = new(4);

        private Event<GameplayInitializeRequest> _gameplayInitializeRequest;
        private Stash<AssetProviderComponent> _providerStash;
        private Filter _assets;
        
        public World World { get; set; }

        public AssetProviderSystem(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public void OnAwake()
        {
            _gameplayInitializeRequest = World.GetEvent<GameplayInitializeRequest>();
            _assets = World.Filter.With<AssetProviderComponent>().With<AssetCleanerComponent>();
            
            LoadAssetsAsync().Forget();
        }

        public void Dispose()
        {
            foreach (AsyncOperationHandle operationHandle in _handles)
                _assetProvider.Release(operationHandle);

            foreach (Entity entity in _assets)
                World.RemoveEntity(entity);
        }

        private async UniTaskVoid LoadAssetsAsync()
        {
            await LoadAsync<PlayerMarker>(AssetAddress.Player);
            await LoadAsync<EnemyMarker>(AssetAddress.Enemy);
            await LoadAsync<LaserMarker>(AssetAddress.Laser);
            await LoadAsync<PickableMarker>(AssetAddress.PickableLaser);
            
            _gameplayInitializeRequest.NextFrame(new GameplayInitializeRequest());
        }

        private async UniTask LoadAsync<T>(string address) where T : struct, IComponent
        {
            AssetLoadResult<GameObject> result = await _assetProvider.LoadAsync<GameObject>(address);
            Entity assetHolder = World.CreateEntity();
            
            assetHolder.SetComponent(new AssetProviderComponent { Result = result });
            assetHolder.AddComponent<AssetCleanerComponent>();
            assetHolder.AddComponent<T>();

            _handles.Add(result.Handle);
        }
    }
}