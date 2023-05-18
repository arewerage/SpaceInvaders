using _Project._Codebase.Core.Assets.Provider;
using Cysharp.Threading.Tasks;

namespace _Project._Codebase.Services.Game.States
{
    public class BootstrapState : BaseState
    {
        private readonly IAssetProvider _assetProvider;

        public BootstrapState(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }
        
        public override void Enter()
        {
            InitializeAsync().Forget();
        }

        private async UniTaskVoid InitializeAsync()
        {
            await _assetProvider.InitializeAsync();
            
            Game.ChangeState<LoadMainMenuState>();
        }
    }
}