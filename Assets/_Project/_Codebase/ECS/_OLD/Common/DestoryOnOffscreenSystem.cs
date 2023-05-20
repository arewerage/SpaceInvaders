using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace _Project._Codebase.ECS._OLD.Common
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class DestoryOnOffscreenSystem : IFixedSystem
    {
        public World World { get; set; }

        private Stash<TransformComponent> _transformStash;
        private Filter _entities;

        public void OnAwake()
        {
            _transformStash = World.GetStash<TransformComponent>();
            _entities = World.Filter.With<DestroyOnOffscreenMarker>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity entity in _entities)
            {
                ref var transform = ref _transformStash.Get(entity);
                
                if (Mathf.Abs(transform.Transform.position.y) > 6f)
                    Object.Destroy(transform.Transform.gameObject);
            }
        }

        public void Dispose()
        {
        }
    }
}