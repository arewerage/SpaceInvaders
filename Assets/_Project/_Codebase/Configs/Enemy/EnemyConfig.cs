using System.Collections.Generic;
using UnityEngine;

namespace _Project._Codebase.Configs.Enemy
{
    [CreateAssetMenu(menuName = "Configs/Enemy", fileName = "EnemyConfig")]
    public class EnemyConfig : ScriptableObject
    {
        [SerializeField] private List<EnemyType> _enemyList;

        public List<EnemyType> EnemyList => _enemyList;
    }
}