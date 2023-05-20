using _Project._Codebase.Constants;
using _Project._Codebase.Core.SceneLoader;
using Cysharp.Threading.Tasks;

namespace _Project._Codebase.Services.Game.States
{
    public class LoadLevelState : BaseState
    {
        private readonly ISceneLoader _sceneLoader;

        public LoadLevelState(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }
        
        public override void Enter()
        {
            LoadAsync().Forget();
        }

        private async UniTaskVoid LoadAsync()
        {
            await _sceneLoader.LoadAsync(AssetAddress.GameplayScene);
            
            Game.ChangeState<InitLevelState>();
        }
    }
}