using _Project._Codebase.ECS.Common;
using _Project._Codebase.ECS.Laser;
using _Project._Codebase.ECS.Physics;
using _Project._Codebase.ECS.UnityRelated;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace _Project._Codebase.ECS.Enemy
{
    using Scellecs.Morpeh;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class EnemyDestroySystem : ISystem
    {
        private Event<TriggerEnterEvent> _triggerEnterEvent;
        private Stash<Rigidbody2dComponent> _rigidbodyStash;
        private Stash<OwnerComponent> _ownerStash;
        private Filter _enemies;
        private Filter _damageableLasers;

        public World World { get; set; }

        public void OnAwake()
        {
            _triggerEnterEvent = World.GetEvent<TriggerEnterEvent>();
            _rigidbodyStash = World.GetStash<Rigidbody2dComponent>();
            _ownerStash = World.GetStash<OwnerComponent>();
            _enemies = World.Filter.With<EnemyMarker>().With<Rigidbody2dComponent>();
            _damageableLasers = World.Filter.With<LaserMarker>().With<OwnerComponent>()
                .With<Rigidbody2dComponent>().Without<PickableComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            if (_triggerEnterEvent.IsPublished == false)
                return;
            
            foreach (TriggerEnterEvent triggerEnterEvent in _triggerEnterEvent.BatchedChanges)
            foreach (Entity enemy in _enemies)
            foreach (Entity damageableLaser in _damageableLasers)
            {
                if (triggerEnterEvent.Target != enemy && triggerEnterEvent.Sender != damageableLaser)
                    continue;

                ref var owner = ref _ownerStash.Get(damageableLaser);

                if (owner.Value == enemy)
                    continue;

                ref var enemyRigidbody = ref _rigidbodyStash.Get(enemy);
                ref var laserRigidbody = ref _rigidbodyStash.Get(damageableLaser);
                
                Object.Destroy(enemyRigidbody.Value.gameObject);
                Object.Destroy(laserRigidbody.Value.gameObject);
                return;
            }
        }

        public void Dispose()
        {
        }
    }
}