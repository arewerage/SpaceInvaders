using UnityEngine.ResourceManagement.AsyncOperations;

namespace _Project._Codebase.Core.AssetProvider
{
    public readonly struct AssetLoadResult<T> where T : class
    {
        public readonly AsyncOperationHandle Handle;
        public readonly T Object;

        public AssetLoadResult(AsyncOperationHandle handle, T obj)
        {
            Handle = handle;
            Object = obj;
        }
    }
}