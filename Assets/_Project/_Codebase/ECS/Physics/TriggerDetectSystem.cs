using _Project._Codebase.ECS.Enemy;
using _Project._Codebase.ECS.Player;
using _Project._Codebase.ECS.UnityRelated;
using Unity.IL2CPP.CompilerServices;

namespace _Project._Codebase.ECS.Physics
{
    using Scellecs.Morpeh;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class TriggerDetectSystem : IFixedSystem
    {
        private Event<TriggerEnterEvent> _triggerEnterEvent;
        private Stash<Rigidbody2dComponent> _rigidbodyStash;
        private Stash<PlayerMarker> _playerMarker;
        private Stash<EnemyMarker> _enemyMarker;
        private Filter _physicsBodies;

        public World World { get; set; }

        public void OnAwake()
        {
            _triggerEnterEvent = World.GetEvent<TriggerEnterEvent>();
            _rigidbodyStash = World.GetStash<Rigidbody2dComponent>();
            _playerMarker = World.GetStash<PlayerMarker>();
            _enemyMarker = World.GetStash<EnemyMarker>();
            _physicsBodies = World.Filter.With<Rigidbody2dComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            if (_triggerEnterEvent.IsPublished == false)
                return;
            
            foreach (TriggerEnterEvent triggerEnterEvent in _triggerEnterEvent.BatchedChanges)
            foreach (Entity physicsBody in _physicsBodies)
            {
                ref var rigidbody = ref _rigidbodyStash.Get(physicsBody);

                Entity target = triggerEnterEvent.Target;

                if (target != physicsBody)
                    return;

                if (_playerMarker.Has(target))
                {
                }
                else if (_enemyMarker.Has(target))
                {
                }
            }
        }

        public void Dispose()
        {
        }
    }
}