using UnityEngine;

namespace _Project._Codebase.Configs.Laser
{
    [System.Serializable]
    public class LaserType
    {
        [SerializeField] private LaserId _laserId;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private float _speed;
        [SerializeField] private Sprite _pickableSprite;

        public LaserId LaserId => _laserId;
        public Sprite Sprite => _sprite;
        public float Speed => _speed;
        public Sprite PickableSprite => _pickableSprite;
    }
}