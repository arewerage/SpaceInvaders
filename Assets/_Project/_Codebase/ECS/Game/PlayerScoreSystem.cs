using _Project._Codebase.Configs;
using Unity.IL2CPP.CompilerServices;

namespace _Project._Codebase.ECS.Game
{
    using Scellecs.Morpeh;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class PlayerScoreSystem : ISystem
    {
        private readonly GameConfig _gameConfig;
        
        private Event<PlayerAddScoreEvent> _playerAddScoreEvent;

        public World World { get; set; }
        
        public PlayerScoreSystem(GameConfig gameConfig)
        {
            _gameConfig = gameConfig;
        }

        public void OnAwake()
        {
            _playerAddScoreEvent = World.GetEvent<PlayerAddScoreEvent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (PlayerAddScoreEvent playerAddScoreEvent in _playerAddScoreEvent.BatchedChanges)
            {
                _gameConfig.PlayerScore.Value += playerAddScoreEvent.Value;
            }
        }

        public void Dispose()
        {
        }
    }
}