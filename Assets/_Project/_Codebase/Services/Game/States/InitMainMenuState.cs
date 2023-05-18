using _Project._Codebase.Configs;
using _Project._Codebase.Services.UI.Screen;
using _Project._Codebase.Services.UI.UIRootProvider;
using Cysharp.Threading.Tasks;

namespace _Project._Codebase.Services.Game.States
{
    public class InitMainMenuState : BaseState
    {
        private readonly IUIRootProvider _uiRootProvider;
        private readonly IScreenService _screenService;

        public InitMainMenuState(IUIRootProvider uiRootProvider, IScreenService screenService)
        {
            _uiRootProvider = uiRootProvider;
            _screenService = screenService;
        }
        
        public override void Enter()
        {
            InitializeAsync().Forget();
        }

        private async UniTaskVoid InitializeAsync()
        {
            await _uiRootProvider.InitializeAsync();
            await _screenService.Show(ScreenId.MainMenu);
        }
    }
}