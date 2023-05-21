using _Project._Codebase.ECS.Common;
using _Project._Codebase.ECS.Extensions;
using _Project._Codebase.ECS.Laser;
using _Project._Codebase.ECS.UnityRelated;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace _Project._Codebase.ECS.Physics
{
    using Scellecs.Morpeh;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class PhysicsCollisionSystem : IFixedSystem
    {
        private Stash<Collider2dComponent> _colliderStash;
        private Filter _bodies;
        
        public World World { get; set; }

        public void OnAwake()
        {
            _colliderStash = World.GetStash<Collider2dComponent>();
            _bodies = World.Filter.With<Rigidbody2dComponent>().With<Collider2dComponent>()
                .Without<TriggeredComponent>().Without<DestroyComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity body in _bodies)
            {
                ref var collider = ref _colliderStash.Get(body);

                RaycastHit2D[] hits = new RaycastHit2D[1];
                ContactFilter2D filter = new() { useTriggers = true,
                    layerMask = LayerMask.GetMask("Player", "Damageable", "Enemy", "Pickable"), useLayerMask = true};

                int hitsCount = collider.Value.Cast(Vector2.zero, filter, hits, 0f, true);

                for (int i = 0; i < hitsCount; i++)
                {
                    if (hits[i].collider.gameObject.TryGetEntity(out Entity entity) == false)
                        return;

                    ref var owner = ref entity.GetComponent<OwnerComponent>(out bool isExist);
                    if (isExist && owner.Value.Equals(body))
                        return;
                    
                    body.SetComponent(new TriggeredComponent { By = entity });
                }
            }
        }

        public void Dispose()
        {
        }
    }
}