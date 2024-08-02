using Game.Scripts.Scenes.MenuScene.Behaviour.States.MainMenu;
using Game.Scripts.Scenes.MenuScene.Behaviour.States.SelectLevel;
using Game.Scripts.Scenes.MenuScene.Behaviour.States.Settings;
using Tools.Runtime.StateBehaviour;
using Zenject;

namespace Game.Scripts.Scenes.MenuScene.Behaviour
{
    public class MenuStateManager : StateManager<MenuState>
    {
        [Inject]
        public MenuStateManager(DiContainer container) : base(new []
        {
            typeof(MainMenuState),
            typeof(SelectLevelState),
            typeof(SettingsState)
        }, container)
        {
        }
    }
}