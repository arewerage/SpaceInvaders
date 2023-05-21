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
        private Event<PlayerDestroyEvent> _playerDestroyEvent;
        private Stash<Rigidbody2dComponent> _rigidbodyStash;

        public World World { get; set; }

        public void OnAwake()
        {
            _playerDestroyEvent = World.GetEvent<PlayerDestroyEvent>();
            _rigidbodyStash = World.GetStash<Rigidbody2dComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            if (_playerDestroyEvent.IsPublished == false)
                return;

            foreach (PlayerDestroyEvent playerDestroyEvent in _playerDestroyEvent.BatchedChanges)
            {
                ref var rigidbody = ref _rigidbodyStash.Get(playerDestroyEvent.Player);
                
                Object.Destroy(rigidbody.Value.gameObject);
            }
        }

        public void Dispose()
        {
        }
    }
}