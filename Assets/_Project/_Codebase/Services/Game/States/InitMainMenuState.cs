using _Project._Codebase.Configs;
using _Project._Codebase.Services.UI.Screen;
using _Project._Codebase.Services.UI.UIRootProvider;
using Cysharp.Threading.Tasks;

namespace _Project._Codebase.Services.Game.States
{
    public class InitMainMenuState : BaseState
    {
        private readonly IUIRootProvider _uiRootProvider;
        private readonly IUIScreenService _uiScreenService;

        public InitMainMenuState(IUIRootProvider uiRootProvider, IUIScreenService uiScreenService)
        {
            _uiRootProvider = uiRootProvider;
            _uiScreenService = uiScreenService;
        }
        
        public override void Enter()
        {
            InitializeAsync().Forget();
        }

        private async UniTaskVoid InitializeAsync()
        {
            await _uiRootProvider.InitializeAsync();
            await _uiScreenService.Show(ScreenId.MainMenu);
        }
    }
}