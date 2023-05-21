using _Project._Codebase.ECS.Common;
using _Project._Codebase.ECS.Extensions;
using _Project._Codebase.ECS.Laser;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using UnityEngine;

namespace _Project._Codebase.ECS.Physics
{
    [AddComponentMenu("ECS/" + nameof(TriggerEventSender))]
    public sealed class TriggerEventSenderProvider : MonoProvider<TriggerEventSender>
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetEntity(out Entity target) == false)
                return;

            ref OwnerComponent owner = ref Entity.GetComponent<OwnerComponent>(out bool isLaserOwned);

            if (Entity is null)
                return;

            if (isLaserOwned)
            {
                Debug.Log("Good!");
                if (owner.Value.ID.Equals(Entity.ID))
                {
                    Debug.Log("Amazing!");
                    return;
                }
            }

            target.SetComponent(new TriggeredComponent { By = Entity });
        }
    }
}