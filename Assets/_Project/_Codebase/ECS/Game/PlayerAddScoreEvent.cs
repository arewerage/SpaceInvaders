using Scellecs.Morpeh;

namespace _Project._Codebase.ECS.Game
{
    [System.Serializable]
    public struct PlayerAddScoreEvent : IEventData
    {
        public int Value;
    }
}