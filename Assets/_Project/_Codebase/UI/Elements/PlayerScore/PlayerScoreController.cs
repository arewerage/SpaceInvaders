using _Project._Codebase.Configs;
using UniRx;
using Zenject;

namespace _Project._Codebase.UI.Elements.PlayerScore
{
    public class PlayerScoreController : IInitializable
    {
        private readonly GameConfig _gameConfig;
        private readonly PlayerScoreView _view;

        public PlayerScoreController(GameConfig gameConfig, PlayerScoreView view)
        {
            _gameConfig = gameConfig;
            _view = view;
        }

        public void Initialize()
        {
            _gameConfig.PlayerScore
                .AsObservable()
                .Subscribe(value => _view.SetScore(value))
                .AddTo(_view);
        }
    }
}