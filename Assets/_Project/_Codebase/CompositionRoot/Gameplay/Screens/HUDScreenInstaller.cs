using _Project._Codebase.UI.Elements.CurrentWave;
using _Project._Codebase.UI.Elements.MenuButton;
using _Project._Codebase.UI.Elements.PlayerScore;
using UnityEngine;
using Zenject;

namespace _Project._Codebase.CompositionRoot.Gameplay.Screens
{
    public class HUDScreenInstaller : MonoInstaller
    {
        [SerializeField] private PlayerScoreView _playerScoreView;
        [SerializeField] private CurrentWaveView _currentWaveView;
        [SerializeField] private MenuButtonView _menuButtonView;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_playerScoreView).AsSingle();
            Container.BindInterfacesTo<PlayerScoreController>().AsSingle();
            
            Container.BindInstance(_currentWaveView).AsSingle();
            Container.BindInterfacesTo<CurrentWaveController>().AsSingle();
            
            Container.BindInstance(_menuButtonView).AsSingle();
            Container.BindInterfacesTo<MenuButtonController>().AsSingle();
        }
    }
}