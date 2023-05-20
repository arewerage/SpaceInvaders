using System;
using _Project._Codebase.Services.Game;
using _Project._Codebase.Services.Game.States;
using UniRx;
using Zenject;

namespace _Project._Codebase.UI.Elements.MenuButton
{
    public class MenuButtonController : IInitializable
    {
        private readonly Game _game;
        private readonly MenuButtonView _view;

        public MenuButtonController(Game game, MenuButtonView view)
        {
            _game = game;
            _view = view;
        }
        
        public void Initialize()
        {
            _view.Button
                .OnClickAsObservable()
                .ThrottleFirst(TimeSpan.FromSeconds(1))
                .Subscribe(_ => _game.ChangeState<LoadMainMenuState>())
                .AddTo(_view);
        }
    }
}