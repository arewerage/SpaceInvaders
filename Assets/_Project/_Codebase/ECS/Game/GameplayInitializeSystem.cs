using _Project._Codebase.Configs;
using _Project._Codebase.ECS.Enemy;
using _Project._Codebase.ECS.Player;
using Unity.IL2CPP.CompilerServices;

namespace _Project._Codebase.ECS.Game
{
    using Scellecs.Morpeh;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class GameplayInitializeSystem : ISystem
    {
        private readonly PlayerConfig _playerConfig;

        private Event<GameplayInitializeRequest> _gameplayInitializeRequest;
        private Event<PlayerSpawnRequest> _playerSpawnRequest;
        private Event<EnemyWaveSpawnRequest> _enemySpawnWaveRequest;

        public World World { get; set; }

        public GameplayInitializeSystem(PlayerConfig playerConfig)
        {
            _playerConfig = playerConfig;
        }

        public void OnAwake()
        {
            _gameplayInitializeRequest = World.GetEvent<GameplayInitializeRequest>();
        }

        public void OnUpdate(float deltaTime)
        {
            if (_gameplayInitializeRequest.IsPublished == false)
                return;
            
            SpawnPlayer();
            SpawnEnemyWave();
        }
        
        public void Dispose()
        {
        }

        private void SpawnPlayer()
        {
            _playerSpawnRequest = World.GetEvent<PlayerSpawnRequest>();
            _playerSpawnRequest.NextFrame(new PlayerSpawnRequest { PlayerConfig = _playerConfig });
        }

        private void SpawnEnemyWave()
        {
            _enemySpawnWaveRequest = World.GetEvent<EnemyWaveSpawnRequest>();
            _enemySpawnWaveRequest.NextFrame(new EnemyWaveSpawnRequest());
        }
    }
}