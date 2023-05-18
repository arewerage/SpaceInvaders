using _Project._Codebase.UI;
using Cysharp.Threading.Tasks;

namespace _Project._Codebase.Services.UI.UIRootProvider
{
    public interface IUIRootProvider
    {
        UIRoot UIRoot { get; }

        UniTask InitializeAsync();
    }
}