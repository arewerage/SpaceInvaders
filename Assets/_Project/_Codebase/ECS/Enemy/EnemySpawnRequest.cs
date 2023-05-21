using _Project._Codebase.Configs.Enemy;
using Scellecs.Morpeh;
using UnityEngine;

namespace _Project._Codebase.ECS.Enemy
{
    [System.Serializable]
    public struct EnemySpawnRequest : IEventData
    {
        public EnemyType EnemyType;
        public Vector2 Position;
    }
}