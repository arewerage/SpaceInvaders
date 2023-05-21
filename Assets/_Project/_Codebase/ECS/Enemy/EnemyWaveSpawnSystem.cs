using System.Collections.Generic;
using System.Linq;
using _Project._Codebase.Configs;
using _Project._Codebase.Configs.Enemy;
using _Project._Codebase.Extensions;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace _Project._Codebase.ECS.Enemy
{
    using Scellecs.Morpeh;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class EnemyWaveSpawnSystem : ISystem
    {
        private readonly GameConfig _gameConfig;
        private readonly Dictionary<EnemyId, EnemyType> _enemyConfig;
        
        private Event<EnemyWaveSpawnRequest> _enemySpawnWaveRequest;
        private Event<EnemySpawnRequest> _enemySpawnRequest;

        public World World { get; set; }

        public EnemyWaveSpawnSystem(GameConfig gameConfig, EnemyConfig enemyConfig)
        {
            _gameConfig = gameConfig;
            _enemyConfig = enemyConfig.EnemyList.ToDictionary(x => x.EnemyId, x => x);
        }

        public void OnAwake()
        {
            _enemySpawnWaveRequest = World.GetEvent<EnemyWaveSpawnRequest>();
            _enemySpawnRequest = World.GetEvent<EnemySpawnRequest>();
        }

        public void OnUpdate(float deltaTime)
        {
            if (_enemySpawnWaveRequest.IsPublished == false)
                return;

            EnemyId enemyId = EnumExtensions.GetRandom<EnemyId>();
            
            Vector2 center = Vector2.up * 2.5f;
            GridInfo grid = _gameConfig.EnemyGridInfo;

            float xOffset = (grid.Columns - 1) * grid.Spacing * 0.5f;
            float yOffset = (grid.Rows - 1) * grid.Spacing * 0.5f;
            
            for (int row = 0; row < grid.Rows; row++)
            for (int column = 0; column < grid.Columns; column++)
            {
                Vector2 position = new(column * grid.Spacing - xOffset, row * grid.Spacing - yOffset);
                _enemySpawnRequest.NextFrame(new EnemySpawnRequest
                {
                    Position = position + center,
                    EnemyType = _enemyConfig[enemyId]
                });
            }
            
            _gameConfig.CurrentWave.Value++;
        }

        public void Dispose()
        {
        }
    }
}