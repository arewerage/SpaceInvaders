using Scellecs.Morpeh;

namespace _Project._Codebase.ECS.Physics
{
    [System.Serializable]
    public struct TriggerEnterEvent : IEventData
    {
        public Entity Sender;
        public Entity Target;
    }
}