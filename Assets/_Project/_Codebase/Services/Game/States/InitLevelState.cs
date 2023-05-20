using _Project._Codebase.Configs;
using _Project._Codebase.ECS;
using _Project._Codebase.Services.UI.Screen;
using _Project._Codebase.Services.UI.UIRootProvider;
using Cysharp.Threading.Tasks;

namespace _Project._Codebase.Services.Game.States
{
    public class InitLevelState : BaseState
    {
        private readonly IUIRootProvider _uiRootProvider;
        private readonly IUIScreenService _uiScreenService;
        private readonly ECSWorld _ecsWorld;

        public InitLevelState(IUIRootProvider uiRootProvider, IUIScreenService uiScreenService, ECSWorld ecsWorld)
        {
            _uiRootProvider = uiRootProvider;
            _uiScreenService = uiScreenService;
            _ecsWorld = ecsWorld;
        }
        
        public override void Enter()
        {
            InitializeAsync().Forget();
        }

        private async UniTaskVoid InitializeAsync()
        {
            await _uiRootProvider.InitializeAsync();
            await _uiScreenService.Show(ScreenId.HUD);
            
            _ecsWorld.Initialize();
            
            Game.ChangeState<GameLoopState>();
        }
    }
}