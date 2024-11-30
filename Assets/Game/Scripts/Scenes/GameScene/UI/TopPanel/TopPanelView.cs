using TMPro;
using Tools.Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Scenes.GameScene.UI.TopPanel
{
    public class TopPanelView : UIMonoBehaviour
    {
        [SerializeField] private Button _howToPlayButton;
        [SerializeField] TMP_Text _usdBalanceText;
        [SerializeField] private Button _changePinsButton;
        [SerializeField] private Button _betHistoryButton;
        
        
        public Button HowToPlayButton => _howToPlayButton;
        public Button ChangePinsButton => _changePinsButton;
        public Button BetHistoryButton => _betHistoryButton;
        
        public void SetBalanceText(string balance)
        {
            _usdBalanceText.text = balance;
        }
    }
}