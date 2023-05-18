using System.Collections.Generic;
using System.Linq;
using _Project._Codebase.Configs;
using _Project._Codebase.Core.Object;
using _Project._Codebase.UI.Screens;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace _Project._Codebase.Services.UI.Screen
{
    public class ScreenService : IScreenService
    {
        private readonly IObjectFactory<IUIScreen, AssetReference> _objectFactory;
        private readonly Dictionary<ScreenId, Configs.Screen> _screenConfig;

        public ScreenService(IObjectFactory<IUIScreen, AssetReference> objectFactory, ScreenConfig screenConfig)
        {
            _objectFactory = objectFactory;
            _screenConfig = screenConfig.ScreenList.ToDictionary(x => x.ScreenId, x => x);
        }
        
        public async UniTask<IUIScreen> Show(ScreenId screenId)
        {
            return await _objectFactory.CreateAsync(_screenConfig[screenId].Asset);
        }
    }
}