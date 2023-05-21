using Scellecs.Morpeh;

namespace _Project._Codebase.ECS.Player
{
    [System.Serializable]
    public struct PlayerDestroyEvent : IEventData
    {
        public Entity Player;
    }
}