using _Project._Codebase.Constants;
using _Project._Codebase.Core.Object;
using _Project._Codebase.UI;
using Cysharp.Threading.Tasks;

namespace _Project._Codebase.Services.UI.UIRootProvider
{
    public class UIRootProvider : IUIRootProvider
    {
        private readonly IObjectFactory<UIRoot, string> _objectFactory;
        
        public UIRoot UIRoot { get; private set; }

        public UIRootProvider(IObjectFactory<UIRoot, string> objectFactory)
        {
            _objectFactory = objectFactory;
        }
        
        public async UniTask InitializeAsync()
        {
            UIRoot = await _objectFactory.CreateAsync(AssetAddress.UIRoot);
        }
    }
}