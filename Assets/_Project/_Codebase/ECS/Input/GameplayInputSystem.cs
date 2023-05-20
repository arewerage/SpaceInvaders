using _Project._Codebase.Services.Input;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace _Project._Codebase.ECS.Input
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class GameplayInputSystem : ISystem
    {
        private readonly IInputService _inputService;

        private Stash<GameplayInputComponent> _inputStash;
        private Filter _input;
        
        public World World { get; set; }

        public GameplayInputSystem(IInputService inputService)
        {
            _inputService = inputService;
        }

        public void OnAwake()
        {
            _inputStash = World.GetStash<GameplayInputComponent>();
            _input = World.Filter.With<GameplayInputComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity entity in _input)
            {
                ref var gameplayInput = ref _inputStash.Get(entity);

                gameplayInput.MoveAxis = _inputService.MoveAxes;
                gameplayInput.IsShooting = _inputService.IsShooting;
            }
        }

        public void Dispose()
        {
        }
    }
}