using _Project._Codebase.ECS.Common;
using _Project._Codebase.ECS.Laser;
using _Project._Codebase.ECS.Pickable;
using Unity.IL2CPP.CompilerServices;

namespace _Project._Codebase.ECS.Player
{
    using Scellecs.Morpeh;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class PlayerHitLaserSystem : ISystem
    {
        private Stash<TriggeredComponent> _triggeredStash;
        private Filter _players;
        private Filter _lasers;

        public World World { get; set; }

        public void OnAwake()
        {
            _triggeredStash = World.GetStash<TriggeredComponent>();
            _players = World.Filter.With<PlayerMarker>().With<TriggeredComponent>().Without<DestroyComponent>();
            _lasers = World.Filter.With<LaserMarker>().Without<PickableMarker>().Without<DestroyComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity player in _players)
            foreach (Entity laser in _lasers)
            {
                ref var triggered = ref _triggeredStash.Get(player);

                if (laser.ID.Equals(triggered.By.ID) == false)
                    continue;

                player.AddComponent<DestroyComponent>();
                laser.AddComponent<DestroyComponent>();
            }
        }

        public void Dispose()
        {
        }
    }
}