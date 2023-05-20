using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Project._Codebase.Configs
{
    [System.Serializable]
    public class Screen
    {
        [SerializeField] private ScreenId _screenId;
        [SerializeField] private AssetReference _asset;

        public ScreenId ScreenId => _screenId;
        public AssetReference Asset => _asset;
    }

    public enum ScreenId
    {
        None = 0,
        MainMenu = 1,
        HUD = 2
    }
}