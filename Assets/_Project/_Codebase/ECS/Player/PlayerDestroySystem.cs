using _Project._Codebase.ECS.Common;
using _Project._Codebase.ECS.Laser;
using _Project._Codebase.ECS.Physics;
using _Project._Codebase.ECS.UnityRelated;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace _Project._Codebase.ECS.Player
{
    using Scellecs.Morpeh;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class PlayerDestroySystem : ISystem
    {
        private Event<TriggerEnterEvent> _triggerEnterEvent;
        private Stash<Rigidbody2dComponent> _rigidbodyStash;
        private Stash<OwnerComponent> _ownerStash;
        private Filter _players;
        private Filter _damageableLasers;
        
        public World World { get; set; }

        public void OnAwake()
        {
            _triggerEnterEvent = World.GetEvent<TriggerEnterEvent>();
            _ownerStash = World.GetStash<OwnerComponent>();
            _rigidbodyStash = World.GetStash<Rigidbody2dComponent>();
            _players = World.Filter.With<PlayerMarker>().With<Rigidbody2dComponent>();
            _damageableLasers = World.Filter.With<LaserMarker>().With<OwnerComponent>()
                .With<Rigidbody2dComponent>().Without<PickableComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            if (_triggerEnterEvent.IsPublished == false)
                return;

            foreach (TriggerEnterEvent triggerEnterEvent in _triggerEnterEvent.BatchedChanges)
            foreach (Entity player in _players)
            foreach (Entity damageableLaser in _damageableLasers)
            {
                if (triggerEnterEvent.Target != player && triggerEnterEvent.Sender != damageableLaser)
                    continue;

                ref var owner = ref _ownerStash.Get(damageableLaser);

                if (owner.Value == player)
                    continue;

                ref var playerRigidbody = ref _rigidbodyStash.Get(player);
                ref var laserRigidbody = ref _rigidbodyStash.Get(damageableLaser);
                
                Object.Destroy(playerRigidbody.Value.gameObject);
                Object.Destroy(laserRigidbody.Value.gameObject);
                return;
            }
        }

        public void Dispose()
        {
        }
    }
}