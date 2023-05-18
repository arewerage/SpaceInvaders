using System;
using _Project._Codebase.Services.Game;
using _Project._Codebase.Services.Game.States;
using UniRx;
using Zenject;

namespace _Project._Codebase.UI.Elements.StartButton
{
    public class StartButtonController : IInitializable
    {
        private readonly Game _game;
        private readonly StartButtonView _startButtonView;

        public StartButtonController(Game game, StartButtonView startButtonView)
        {
            _game = game;
            _startButtonView = startButtonView;
        }
        
        public void Initialize()
        {
            _startButtonView.Button
                .OnClickAsObservable()
                .ThrottleFirst(TimeSpan.FromSeconds(1))
                .Subscribe(_ => _game.ChangeState<LoadGameplayState>())
                .AddTo(_startButtonView);
        }
    }
}