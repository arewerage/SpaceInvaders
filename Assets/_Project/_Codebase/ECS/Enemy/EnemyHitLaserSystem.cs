using _Project._Codebase.Configs;
using _Project._Codebase.ECS.Common;
using _Project._Codebase.ECS.Game;
using _Project._Codebase.ECS.Pickable;
using _Project._Codebase.ECS.UnityRelated;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace _Project._Codebase.ECS.Enemy
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class EnemyHitLaserSystem : ISystem
    {
        private Event<PickableLaserSpawnRequest> _pickableLaserSpawnRequest;
        private Event<PlayerAddScoreEvent> _playerAddScoreEvent;
        private Stash<TriggeredComponent> _triggeredStash;
        private Stash<Rigidbody2dComponent> _rigidbodyStash;
        private Filter _enemies;

        public World World { get; set; }

        public void OnAwake()
        {
            _pickableLaserSpawnRequest = World.GetEvent<PickableLaserSpawnRequest>();
            _playerAddScoreEvent = World.GetEvent<PlayerAddScoreEvent>();
            _triggeredStash = World.GetStash<TriggeredComponent>();
            _rigidbodyStash = World.GetStash<Rigidbody2dComponent>();
            _enemies = World.Filter.With<EnemyMarker>().With<Rigidbody2dComponent>()
                .With<TriggeredComponent>().Without<DestroyComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity enemy in _enemies)
            {
                ref var triggered = ref _triggeredStash.Get(enemy);
                ref var rigidbody = ref _rigidbodyStash.Get(enemy);
                
                if (triggered.By.Has<PickableMarker>())
                    continue;

                _playerAddScoreEvent.NextFrame(new PlayerAddScoreEvent { Value = 1 });
                
                if (Random.Range(0f, 1f) > 0.8)
                    _pickableLaserSpawnRequest.NextFrame(new PickableLaserSpawnRequest { Position = rigidbody.Value.position });
                
                enemy.AddComponent<DestroyComponent>();
                triggered.By.AddComponent<DestroyComponent>();
            }
        }

        public void Dispose()
        {
        }
    }
}