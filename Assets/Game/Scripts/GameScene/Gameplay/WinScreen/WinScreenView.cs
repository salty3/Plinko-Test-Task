using TMPro;
using Tools.Runtime;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Scripts.GameScene.Gameplay.WinScreen
{
    public class WinScreenView : UIMonoBehaviour
    {
        [SerializeField] private TMP_Text _playedTimeText;
        [SerializeField] private Button _playNextLevelButton;
        [SerializeField] private Button _toMainMenuButton;

        public UnityEvent PlayNextLevelClicked => _playNextLevelButton.onClick;
        public UnityEvent ToMainMenuClicked => _toMainMenuButton.onClick;

        public void HidePlayNextLevelButton()
        {
            _playNextLevelButton.gameObject.SetActive(false);
        }
        
        public void SetPlayedTime(string text)
        {
            _playedTimeText.text = text;
        }
        
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