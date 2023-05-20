using UnityEngine;

namespace _Project._Codebase.Configs
{
    [CreateAssetMenu(menuName = "Configs/EntitiesSpeed", fileName = "EntitiesSpeedConfig")]
    public class EntitiesSpeedConfig : ScriptableObject
    {
        [SerializeField] private float _playerSpeed = 5f;
        [SerializeField] private float _shootingLaserSpeed = 10f;
        [SerializeField] private float _pickableLaserSpeed = 4f;

        public float PlayerSpeed => _playerSpeed;
        public float ShootingLaserSpeed => _shootingLaserSpeed;
        public float PickableLaserSpeed => _pickableLaserSpeed;
    }
}