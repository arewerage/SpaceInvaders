using Scellecs.Morpeh;
using UnityEngine;

namespace _Project._Codebase.ECS.Laser
{
    [System.Serializable]
    public struct LaserSpawnRequest : IEventData
    {
        public Entity Owner;
        public Vector3 Position;
        public float Angle;
    }
}