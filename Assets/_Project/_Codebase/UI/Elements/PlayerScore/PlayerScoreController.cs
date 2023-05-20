using Zenject;

namespace _Project._Codebase.UI.Elements.PlayerScore
{
    public class PlayerScoreController : IInitializable
    {
        private readonly PlayerScoreView _view;

        public PlayerScoreController(PlayerScoreView view)
        {
            _view = view;
        }

        public void Initialize()
        {
            // _playerScoreChangeObservable.Observable
            //     .Subscribe(value => _view.SetScore(value))
            //     .AddTo(_view);
        }
    }
}