using Tools.Runtime;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Scripts.Scenes.GameScene.UI.LoseScreen
{
    public class LoseScreenView : UIMonoBehaviour
    {
        [SerializeField] private Button _retryLevelButton;
        [SerializeField] private Button _toMainMenuButton;

        public UnityEvent RetryLevelClicked => _retryLevelButton.onClick;
        public UnityEvent ToMainMenuClicked => _toMainMenuButton.onClick;
        
        public void Show()
        {
            GameObject.SetActive(true);
        }
        
        public void Hide()
        {
            if (this == null)
            {
                return;
            }
            
            GameObject.SetActive(false);
        }
    }
}