using Cysharp.Threading.Tasks;

namespace _Project._Codebase.Core.Object
{
    public interface IObjectFactory<TResult, in TParam>
    {
        UniTask<TResult> CreateAsync(TParam param);
    }
}