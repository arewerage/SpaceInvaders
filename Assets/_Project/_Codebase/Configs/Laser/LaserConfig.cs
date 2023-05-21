using System.Collections.Generic;
using UnityEngine;

namespace _Project._Codebase.Configs.Laser
{
    [CreateAssetMenu(menuName = "Configs/Laser", fileName = "LaserConfig")]
    public class LaserConfig : ScriptableObject
    {
        [SerializeField] private List<LaserType> _laserList;

        public List<LaserType> LaserList => _laserList;
    }
}