using _Project._Codebase.ECS._OLD.Common;
using _Project._Codebase.ECS._OLD.Laser;
using _Project._Codebase.Services.Input;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace _Project._Codebase.ECS._OLD.Player
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class PlayerShootingSystem : ISystem
    {
        private readonly IInputService _inputService;

        private Event<LaserSpawnEvent> _laserSpawnEvent;
        private Stash<TransformComponent> _transformStash;
        private Filter _players;
        
        public World World { get; set; }

        public PlayerShootingSystem(IInputService inputService)
        {
            _inputService = inputService;
        }

        public void OnAwake()
        {
            _laserSpawnEvent = World.GetEvent<LaserSpawnEvent>();
            _transformStash = World.GetStash<TransformComponent>();
            _players = World.Filter.With<PlayerMarker>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity player in _players)
            {
                ref var transform = ref _transformStash.Get(player);

                if (_inputService.IsShooting == false)
                    return;
                
                _laserSpawnEvent.NextFrame(new LaserSpawnEvent
                {
                    Owner = player,
                    Direction = Vector2.up,
                    Transform = transform.Transform,
                    IsLookDown = false
                });
            }
        }

        public void Dispose()
        {
        }
    }
}