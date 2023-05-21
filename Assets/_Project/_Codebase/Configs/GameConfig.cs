using UniRx;
using UnityEngine;

namespace _Project._Codebase.Configs
{
    [CreateAssetMenu(menuName = "Configs/Game", fileName = "GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [SerializeField] private GridInfo _enemyGridInfo;
        
        public ReactiveProperty<int> PlayerScore { get; } = new(0);
        public ReactiveProperty<int> CurrentWave { get; } = new(0);
        public GridInfo EnemyGridInfo => _enemyGridInfo;
    }

    [System.Serializable]
    public class GridInfo
    {
        [Range(1f, 7f)] public int Columns;
        [Range(1f, 3f)] public int Rows;
        [Range(1f, 1.5f)] public float Spacing;
    }
}