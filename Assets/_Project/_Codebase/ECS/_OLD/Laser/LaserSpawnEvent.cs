using System;
using Scellecs.Morpeh;
using UnityEngine;

namespace _Project._Codebase.ECS._OLD.Laser
{
    [Serializable]
    public struct LaserSpawnEvent : IEventData {
        public Entity Owner;
        public Transform Transform;
        public Vector2 Direction;
        public bool IsLookDown;
    }
}