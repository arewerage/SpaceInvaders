using _Project._Codebase.ECS.Common;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace _Project._Codebase.ECS.Player
{
    using Scellecs.Morpeh;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class PlayerHitLaserSystem : ISystem
    {
        private Event<PlayerDestroyEvent> _playerDestroyEvent;
        private Stash<TriggeredComponent> _triggeredStash;
        private Filter _players;

        public World World { get; set; }

        public void OnAwake()
        {
            _playerDestroyEvent = World.GetEvent<PlayerDestroyEvent>();
            _triggeredStash = World.GetStash<TriggeredComponent>();
            _players = World.Filter.With<PlayerMarker>().With<TriggeredComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity player in _players)
            {
                ref var triggered = ref _triggeredStash.Get(player);

                if (player == triggered.By)
                    continue;

                if (triggered.By.Has<PickableComponent>())
                    continue;
                
                _playerDestroyEvent.NextFrame(new PlayerDestroyEvent { Player = player });

                player.RemoveComponent<TriggeredComponent>();
            }
        }

        public void Dispose()
        {
        }
    }
}