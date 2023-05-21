using _Project._Codebase.Configs.Laser;
using UnityEngine;

namespace _Project._Codebase.Configs
{
    [CreateAssetMenu(menuName = "Configs/EntitiesSpeed", fileName = "EntitiesSpeedConfig")]
    public class PlayerConfig : ScriptableObject
    {
        [SerializeField] private Sprite _sprite;
        [SerializeField] private float _speed = 5f;
        [SerializeField] private LaserId _laserId;

        public Sprite Sprite => _sprite;
        public float Speed => _speed;
        public LaserId LaserId => _laserId;
    }
}