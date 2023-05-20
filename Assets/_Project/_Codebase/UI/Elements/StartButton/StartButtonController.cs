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
        private readonly StartButtonView _view;

        public StartButtonController(Game game, StartButtonView view)
        {
            _game = game;
            _view = view;
        }
        
        public void Initialize()
        {
            _view.Button
                .OnClickAsObservable()
                .ThrottleFirst(TimeSpan.FromSeconds(1))
                .Subscribe(_ => _game.ChangeState<LoadLevelState>())
                .AddTo(_view);
        }
    }
}