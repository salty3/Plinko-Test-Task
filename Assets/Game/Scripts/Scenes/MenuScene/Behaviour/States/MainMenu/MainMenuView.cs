using Tools.Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Scenes.MenuScene.Behaviour.States.MainMenu
{
    public class MainMenuView : UIMonoBehaviour
    {
        [SerializeField] private Button _selectLevelButton;
        [SerializeField] private Button _settingsButton;
        
        public Button SelectLevelButton => _selectLevelButton;
        public Button SettingsButton => _settingsButton;
        
        public void Show()
        {
            gameObject.SetActive(true);
        }
        
        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}