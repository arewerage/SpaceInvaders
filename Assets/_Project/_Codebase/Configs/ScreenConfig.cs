using System.Collections.Generic;
using UnityEngine;

namespace _Project._Codebase.Configs
{
    [CreateAssetMenu(menuName = "Configs/Screen", fileName = "ScreenConfig")]
    public class ScreenConfig : ScriptableObject
    {
        [SerializeField] private List<Screen> _screenList;

        public List<Screen> ScreenList => _screenList;
    }
}