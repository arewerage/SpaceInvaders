using _Project._Codebase.Configs;
using _Project._Codebase.Constants;
using _Project._Codebase.Core.Object;
using _Project._Codebase.ECS.Common;
using _Project._Codebase.ECS.Extensions;
using _Project._Codebase.ECS.UnityRelated;
using Cysharp.Threading.Tasks;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace _Project._Codebase.ECS.Enemy
{
    using Scellecs.Morpeh;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class EnemySpawnSystem : IInitializer
    {
        private readonly GameConfig _gameConfig;
        private readonly IObjectFactory<MonoBehaviour, string> _objectFactory;

        public World World { get; set; }

        public EnemySpawnSystem(GameConfig gameConfig, IObjectFactory<MonoBehaviour, string> objectFactory)
        {
            _gameConfig = gameConfig;
            _objectFactory = objectFactory;
        }

        public void OnAwake()
        {
            Vector2 center = Vector2.up * 2.5f;
            GridInfo grid = _gameConfig.EnemyGridInfo;

            float xOffset = (grid.Columns - 1) * grid.Spacing * 0.5f;
            float yOffset = (grid.Rows - 1) * grid.Spacing * 0.5f;
            
            for (int row = 0; row < grid.Rows; row++)
            for (int column = 0; column < grid.Columns; column++)
            {
                Vector2 position = new(column * grid.Spacing - xOffset, row * grid.Spacing - yOffset);
                SpawnAsync(position + center).Forget();
            }
        }

        public void Dispose()
        {
        }

        private async UniTaskVoid SpawnAsync(Vector2 position)
        {
            MonoBehaviour enemyObject = await _objectFactory.CreateAsync(AssetAddress.Enemy);
            
            if (enemyObject.TryGetEntity(out Entity enemyEntity) == false)
                return;
            
            enemyEntity.SetComponent(new EnemyMarker());
            enemyEntity.SetComponent(new VelocityComponent { Value = Vector2.zero });

            var rigidbody = enemyEntity.GetComponent<Rigidbody2dComponent>().Value;
            rigidbody.position = position;
            rigidbody.rotation = 180f;
        }
    }
}