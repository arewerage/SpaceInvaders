using _Project._Codebase.ECS.Common;
using _Project._Codebase.ECS.Pickable;
using Unity.IL2CPP.CompilerServices;

namespace _Project._Codebase.ECS.Player
{
    using Scellecs.Morpeh;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class PlayerPickupLaserSystem : ISystem
    {
        private Stash<TriggeredComponent> _triggeredStash;
        private Stash<LaserWeaponTypeComponent> _laserWeaponTypeStash;
        private Filter _players;
        
        public World World { get; set; }

        public void OnAwake()
        {
            _triggeredStash = World.GetStash<TriggeredComponent>();
            _laserWeaponTypeStash = World.GetStash<LaserWeaponTypeComponent>();
            _players = World.Filter.With<PlayerMarker>().With<LaserWeaponTypeComponent>()
                .With<TriggeredComponent>().Without<DestroyComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity player in _players)
            {
                ref var triggered = ref _triggeredStash.Get(player);
                ref var laserWeaponType = ref _laserWeaponTypeStash.Get(triggered.By);

                if (triggered.By.Has<PickableMarker>() == false)
                    continue;
                
                player.SetComponent(new LaserWeaponTypeComponent { LaserId = laserWeaponType.LaserId });
                triggered.By.AddComponent<DestroyComponent>();
                player.RemoveComponent<TriggeredComponent>();
            }
        }

        public void Dispose()
        {
        }
    }
}