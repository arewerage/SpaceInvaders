using _Project._Codebase.ECS.Input;
using _Project._Codebase.ECS.Laser;
using _Project._Codebase.ECS.UnityRelated;
using Unity.IL2CPP.CompilerServices;

namespace _Project._Codebase.ECS.Player
{
    using Scellecs.Morpeh;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class PlayerShootingSystem : ISystem
    {
        private Event<LaserSpawnRequest> _laserSpawnRequest;
        private Stash<GameplayInputComponent> _gameplayInputStash;
        private Stash<Rigidbody2dComponent> _rigidbodyStash;
        private Filter _players;
        
        public World World { get; set; }

        public void OnAwake()
        {
            _laserSpawnRequest = World.GetEvent<LaserSpawnRequest>();
            _gameplayInputStash = World.GetStash<GameplayInputComponent>();
            _rigidbodyStash = World.GetStash<Rigidbody2dComponent>();
            _players = World.Filter.With<PlayerMarker>().With<GameplayInputComponent>().With<Rigidbody2dComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity player in _players)
            {
                ref var gameplayInput = ref _gameplayInputStash.Get(player);
                ref var rigidbody = ref _rigidbodyStash.Get(player);

                if (gameplayInput.IsShooting == false)
                    return;
                
                _laserSpawnRequest.NextFrame(new LaserSpawnRequest
                {
                    Owner = player,
                    Position = rigidbody.Value.position,
                    Angle = rigidbody.Value.rotation
                });
            }
        }

        public void Dispose()
        {
        }
    }
}