using UnityEngine.ResourceManagement.AsyncOperations;

namespace _Project._Codebase.Core.Assets
{
    public interface IAssetReleasable
    {
        void SetAsset(AsyncOperationHandle handle);
    }
}