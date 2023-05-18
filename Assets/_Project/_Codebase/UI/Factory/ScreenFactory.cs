using _Project._Codebase.Core.Assets;
using _Project._Codebase.Core.Assets.Provider;
using _Project._Codebase.Core.Object;
using _Project._Codebase.Services.UI.UIRootProvider;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace _Project._Codebase.UI.Factory
{
    public class ScreenFactory<TResult, TMono> : IObjectFactory<TResult, AssetReference>
        where TMono : MonoBehaviour, TResult
    {
        private readonly IInstantiator _instantiator;
        private readonly IAssetProvider _assetProvider;
        private readonly IUIRootProvider _uiRootProvider;

        public ScreenFactory(IInstantiator instantiator, IAssetProvider assetProvider, IUIRootProvider uiRootProvider)
        {
            _instantiator = instantiator;
            _assetProvider = assetProvider;
            _uiRootProvider = uiRootProvider;
        }

        public async UniTask<TResult> CreateAsync(AssetReference assetReference)
        {
            AssetLoadResult<GameObject> assetLoadResult = await _assetProvider.LoadAsync<GameObject>(assetReference);

            TMono obj = _instantiator.InstantiatePrefabForComponent<TMono>(assetLoadResult.Object, _uiRootProvider.UIRoot.transform);

            if (obj.TryGetComponent(out IAssetReleasable assetReleasable))
                assetReleasable.SetAsset(assetLoadResult.Handle);

            return obj;
        }
    }
}