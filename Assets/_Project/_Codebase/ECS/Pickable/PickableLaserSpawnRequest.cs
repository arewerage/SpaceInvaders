using Scellecs.Morpeh;
using UnityEngine;

namespace _Project._Codebase.ECS.Pickable
{
    [System.Serializable]
    public struct PickableLaserSpawnRequest : IEventData
    {
        public Vector2 Position;
    }
}