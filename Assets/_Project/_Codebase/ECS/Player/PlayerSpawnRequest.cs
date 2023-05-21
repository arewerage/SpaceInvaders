using _Project._Codebase.Configs;
using Scellecs.Morpeh;

namespace _Project._Codebase.ECS.Player
{
    [System.Serializable]
    public struct PlayerSpawnRequest : IEventData
    {
        public PlayerConfig PlayerConfig;
    }
}