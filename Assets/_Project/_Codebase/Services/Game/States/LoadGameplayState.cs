﻿using _Project._Codebase.Constants;
using _Project._Codebase.Core.SceneLoader;
using Cysharp.Threading.Tasks;

namespace _Project._Codebase.Services.Game.States
{
    public class LoadGameplayState : BaseState
    {
        private readonly ISceneLoader _sceneLoader;

        public LoadGameplayState(ISceneLoader sceneLoader)
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
            
            Game.ChangeState<InitGameplayState>();
        }
    }
}