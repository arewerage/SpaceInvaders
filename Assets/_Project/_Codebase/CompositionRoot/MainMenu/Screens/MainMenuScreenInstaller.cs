using _Project._Codebase.UI.Elements.StartButton;
using UnityEngine;
using Zenject;

namespace _Project._Codebase.CompositionRoot.MainMenu.Screens
{
    public class MainMenuScreenInstaller : MonoInstaller
    {
        [SerializeField] private StartButtonView _startButtonView;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_startButtonView).AsSingle();
            Container.BindInterfacesTo<StartButtonController>().AsSingle();
        }
    }
}