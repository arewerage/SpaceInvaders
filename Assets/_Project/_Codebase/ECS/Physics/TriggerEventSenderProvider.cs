using _Project._Codebase.ECS.Extensions;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using UnityEngine;

namespace _Project._Codebase.ECS.Physics
{
    [AddComponentMenu("ECS/" + nameof(TriggerEventSender))]
    public sealed class TriggerEventSenderProvider : MonoProvider<TriggerEventSender>
    {
        private Entity _currentEntity;
        private Event<TriggerEnterEvent> _triggerEnterEvent;

        private void Awake()
        {
            _currentEntity = Entity;
            _triggerEnterEvent = World.Default.GetEvent<TriggerEnterEvent>();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetEntity(out Entity target) == false)
                return;
            
            _triggerEnterEvent.NextFrame(new TriggerEnterEvent
            {
                Sender = _currentEntity,
                Target = target
            });
        }
    }
}