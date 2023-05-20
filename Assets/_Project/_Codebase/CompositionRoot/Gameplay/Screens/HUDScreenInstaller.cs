using _Project._Codebase.UI.Elements.MenuButton;
using _Project._Codebase.UI.Elements.PlayerScore;
using _Project._Codebase.UI.Elements.RestartButton;
using UnityEngine;
using Zenject;

namespace _Project._Codebase.CompositionRoot.Gameplay.Screens
{
    public class HUDScreenInstaller : MonoInstaller
    {
        [SerializeField] private PlayerScoreView _playerScoreView;
        [SerializeField] private RestartButtonView _restartButtonView;
        [SerializeField] private MenuButtonView _menuButtonView;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_playerScoreView).AsSingle();
            Container.BindInterfacesTo<PlayerScoreController>().AsSingle();

            Container.BindInstance(_restartButtonView).AsSingle();
            Container.BindInterfacesTo<RestartButtonController>().AsSingle();
            
            Container.BindInstance(_menuButtonView).AsSingle();
            Container.BindInterfacesTo<MenuButtonController>().AsSingle();
        }
    }
}