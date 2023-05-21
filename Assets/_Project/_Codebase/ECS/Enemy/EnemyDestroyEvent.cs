﻿using Scellecs.Morpeh;

namespace _Project._Codebase.ECS.Enemy
{
    [System.Serializable]
    public struct EnemyDestroyEvent : IEventData
    {
        public Entity Enemy;
    }
}