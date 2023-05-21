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
        private Event<EnemyDestroyEvent> _enemyDestroyEvent;
        private Stash<Rigidbody2dComponent> _rigidbodyStash;

        public World World { get; set; }

        public void OnAwake()
        {
            _enemyDestroyEvent = World.GetEvent<EnemyDestroyEvent>();
            _rigidbodyStash = World.GetStash<Rigidbody2dComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            if (_enemyDestroyEvent.IsPublished == false)
                return;

            foreach (EnemyDestroyEvent enemyDestroyEvent in _enemyDestroyEvent.BatchedChanges)
            {
                ref var rigidbody = ref _rigidbodyStash.Get(enemyDestroyEvent.Enemy);
                
                Object.Destroy(rigidbody.Value.gameObject);
            }
        }

        public void Dispose()
        {
        }
    }
}