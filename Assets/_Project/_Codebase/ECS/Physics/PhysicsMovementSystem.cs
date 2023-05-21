using _Project._Codebase.ECS.Common;
using _Project._Codebase.ECS.UnityRelated;
using Unity.IL2CPP.CompilerServices;

namespace _Project._Codebase.ECS.Physics
{
    using Scellecs.Morpeh;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class PhysicsMovementSystem : IFixedSystem
    {
        private Stash<Rigidbody2dComponent> _rigidbodyStash;
        private Stash<VelocityComponent> _velocityStash;
        private Stash<SpeedComponent> _speedStash;
        private Filter _entities;
        
        public World World { get; set; }

        public void OnAwake()
        {
            _rigidbodyStash = World.GetStash<Rigidbody2dComponent>();
            _velocityStash = World.GetStash<VelocityComponent>();
            _speedStash = World.GetStash<SpeedComponent>();
            _entities = World.Filter.With<Rigidbody2dComponent>().With<VelocityComponent>().With<SpeedComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity entity in _entities)
            {
                ref var rigidbody = ref _rigidbodyStash.Get(entity);
                ref var velocity = ref _velocityStash.Get(entity);
                ref var speed = ref _speedStash.Get(entity);

                rigidbody.Value.velocity = velocity.Value * speed.Value;
            }
        }

        public void Dispose()
        {
        }
    }
}