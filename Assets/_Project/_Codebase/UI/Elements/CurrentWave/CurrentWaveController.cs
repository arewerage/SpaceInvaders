using _Project._Codebase.Configs;
using UniRx;
using Zenject;

namespace _Project._Codebase.UI.Elements.CurrentWave
{
    public class CurrentWaveController : IInitializable
    {
        private readonly GameConfig _gameConfig;
        private readonly CurrentWaveView _view;

        public CurrentWaveController(GameConfig gameConfig, CurrentWaveView view)
        {
            _gameConfig = gameConfig;
            _view = view;
        }

        public void Initialize()
        {
            _gameConfig.CurrentWave
                .AsObservable()
                .Subscribe(value => _view.SetWave(value))
                .AddTo(_view);
        }
    }
}