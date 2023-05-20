using _Project._Codebase.ECS.Common;
using _Project._Codebase.ECS.Input;
using Unity.IL2CPP.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

namespace _Project._Codebase.ECS.Player
{
    using Scellecs.Morpeh;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class PlayerVelocitySystem : ISystem
    {
        private Stash<GameplayInputComponent> _gameplayInputStash;
        private Stash<VelocityComponent> _velocityStash;
        private Filter _players;
        
        public World World { get; set; }

        public void OnAwake()
        {
            _gameplayInputStash = World.GetStash<GameplayInputComponent>();
            _velocityStash = World.GetStash<VelocityComponent>();
            _players = World.Filter.With<PlayerMarker>().With<GameplayInputComponent>().With<VelocityComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity player in _players)
            {
                ref var gameplayInput = ref _gameplayInputStash.Get(player);
                ref var velocity = ref _velocityStash.Get(player);

                velocity.Value = gameplayInput.MoveAxis;
            }
        }

        public void Dispose()
        {
        }
    }
}