using Game.Scripts.MenuScene.States.Settings;
using UnityEngine;
using Zenject;

namespace Game.Scripts.MenuScene
{
    public class MenuStatesInstaller : MonoInstaller
    {
        [SerializeField] private MainMenuView _mainMenuView;
        [SerializeField] private SettingsView _settingsView;
        [SerializeField] private SelectLevelScreenView _selectLevelView;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_mainMenuView);
            Container.BindInstance(_settingsView);
            Container.BindInstance(_selectLevelView);

            Container.BindInterfacesAndSelfTo<MenuStateManager>().AsSingle().NonLazy();
        }
    }
}