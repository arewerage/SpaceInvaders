using _Project._Codebase.Configs.Laser;
using UnityEngine;

namespace _Project._Codebase.Configs.Enemy
{
    [System.Serializable]
    public class EnemyType
    {
        [SerializeField] private EnemyId _enemyId;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private LaserId _laserId;

        public EnemyId EnemyId => _enemyId;
        public Sprite Sprite => _sprite;
        public LaserId LaserId => _laserId;
    }
}