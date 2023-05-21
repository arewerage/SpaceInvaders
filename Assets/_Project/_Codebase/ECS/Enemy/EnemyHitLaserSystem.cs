using _Project._Codebase.ECS.Common;
using Unity.IL2CPP.CompilerServices;

namespace _Project._Codebase.ECS.Enemy
{
    using Scellecs.Morpeh;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class EnemyHitLaserSystem : ISystem
    {
        private Event<EnemyDestroyEvent> _enemyDestroyEvent;
        private Stash<TriggeredComponent> _triggeredStash;
        private Filter _enemy;

        public World World { get; set; }

        public void OnAwake()
        {
            _enemyDestroyEvent = World.GetEvent<EnemyDestroyEvent>();
            _triggeredStash = World.GetStash<TriggeredComponent>();
            _enemy = World.Filter.With<EnemyMarker>().With<TriggeredComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity enemy in _enemy)
            {
                ref var triggered = ref _triggeredStash.Get(enemy);

                if (triggered.By.Has<PickableComponent>())
                    continue;
                
                _enemyDestroyEvent.NextFrame(new EnemyDestroyEvent { Enemy = enemy });

                enemy.RemoveComponent<TriggeredComponent>();
            }
        }

        public void Dispose()
        {
        }
    }
}