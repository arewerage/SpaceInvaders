using _Project._Codebase.ECS.UnityRelated;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace _Project._Codebase.ECS.Common
{
    using Scellecs.Morpeh;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class DestroyingOnOffscreenSystem : IFixedSystem
    {
        private Stash<Rigidbody2dComponent> _rigidbodyStash;
        private Filter _entities;
        
        public World World { get; set; }

        public void OnAwake()
        {
            _rigidbodyStash = World.GetStash<Rigidbody2dComponent>();
            _entities = World.Filter.With<Rigidbody2dComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity entity in _entities)
            {
                ref var rigidbody = ref _rigidbodyStash.Get(entity);

                if (Mathf.Abs(rigidbody.Value.position.y) > 7f)
                    Object.Destroy(rigidbody.Value.gameObject);
            }
        }

        public void Dispose()
        {
        }
    }
}