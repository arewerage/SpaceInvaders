using _Project._Codebase.Core.Assets.Provider;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

namespace _Project._Codebase.Core.Assets
{
    public class AssetCleaner : MonoBehaviour, IAssetReleasable
    {
        private IAssetProvider _assetProvider;
        private AsyncOperationHandle? _handle;
        
        [Inject]
        private void Construct(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }
        
        public void SetAsset(AsyncOperationHandle handle)
        {
            _handle = handle;
        }

        private void OnDestroy()
        {
            if (_handle is {} handle)
                _assetProvider.Release(handle);
        }
    }
}