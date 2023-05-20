using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace _Project._Codebase.ECS._OLD.Common
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class CommonMovementSystem : IFixedSystem
    {
        private Stash<RigidbodyComponent> _rigidbodyStash;
        private Stash<SpeedComponent> _speedStash;
        private Stash<MoveDirectionComponent> _moveDirectionStash;
        private Filter _players;
        
        public World World { get; set; }

        public void OnAwake()
        {
            _rigidbodyStash = World.GetStash<RigidbodyComponent>();
            _speedStash = World.GetStash<SpeedComponent>();
            _moveDirectionStash = World.GetStash<MoveDirectionComponent>();
            _players = World.Filter.With<RigidbodyComponent>()
                .With<SpeedComponent>().With<MoveDirectionComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity player in _players)
            {
                ref var rigidbody = ref _rigidbodyStash.Get(player);
                ref var speed = ref _speedStash.Get(player);
                ref var moveDirection = ref _moveDirectionStash.Get(player);

                rigidbody.Body.velocity = moveDirection.Direction * speed.Speed;
            }
        }

        public void Dispose()
        {
        }
    }
}