using System;
using _Project._Codebase.Services.Game;
using UniRx;
using Zenject;

namespace _Project._Codebase.UI.Elements.RestartButton
{
    public class RestartButtonController : IInitializable
    {
        private readonly Game _game;
        private readonly RestartButtonView _view;

        public RestartButtonController(Game game, RestartButtonView view)
        {
            _game = game;
            _view = view;
        }
        
        public void Initialize()
        {
            _view.Button
                .OnClickAsObservable()
                .ThrottleFirst(TimeSpan.FromSeconds(1))
                .Subscribe(_ => _game.Restart())
                .AddTo(_view);
        }
    }
}