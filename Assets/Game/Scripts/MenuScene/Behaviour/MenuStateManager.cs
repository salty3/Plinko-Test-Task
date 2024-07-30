using Game.Scripts.MenuScene.States.Settings;
using Tools.Runtime.StateBehaviour;
using Zenject;

namespace Game.Scripts.MenuScene
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