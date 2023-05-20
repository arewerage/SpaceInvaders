using _Project._Codebase.ECS._OLD.Common;
using _Project._Codebase.Services.Input;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace _Project._Codebase.ECS._OLD.Player
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class PlayerInputSystem : ISystem
    {
        private readonly IInputService _inputService;

        private Stash<MoveDirectionComponent> _moveDirectionStash;
        private Filter _players;

        public World World { get; set; }

        public PlayerInputSystem(IInputService inputService)
        {
            _inputService = inputService;
        }

        public void OnAwake()
        {
            _moveDirectionStash = World.GetStash<MoveDirectionComponent>();
            _players = World.Filter.With<PlayerMarker>().With<MoveDirectionComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity player in _players)
            {
                ref var moveDirection = ref _moveDirectionStash.Get(player);
                
                moveDirection.Direction = _inputService.MoveAxes;
            }
        }

        public void Dispose()
        {
        }
    }
}