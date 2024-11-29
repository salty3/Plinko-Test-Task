using UnityEngine;
using Zenject;

namespace Game.Scripts.Scenes.MenuScene.Behaviour.States.MainMenu
{
    public class MainMenuState : MenuState
    {
        private readonly MainMenuView _view;
        private readonly MenuStateManager _menuStateManage;
        
        [Inject]
        public MainMenuState(MainMenuView view, MenuStateManager menuStateManage)
        {
            _view = view;
            _menuStateManage = menuStateManage;
        }
        
        public override void Initialize()
        {
            _view.SettingsButton.onClick.AddListener(OpenSettings);
            
            _view.Show();
        }

        public override void Dispose()
        {
            _view.SettingsButton.onClick.RemoveListener(OpenSettings);
            
            _view.Hide();
        }
        
        private void OpenSettings()
        {
           
        }
    }
}