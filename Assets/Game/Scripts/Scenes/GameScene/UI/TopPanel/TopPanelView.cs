using TMPro;
using Tools.Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Scenes.GameScene.UI.TopPanel
{
    public class TopPanelView : UIMonoBehaviour
    {
        [SerializeField] private TMP_Text _usdBalanceText;
        [SerializeField] private TMP_Text _pinsAmountText;
        [SerializeField] private Button _changePinsButton;
        
        
        public Button ChangePinsButton => _changePinsButton;
        
        public void SetBalanceText(string balance)
        {
            _usdBalanceText.text = balance;
        }
        
        public void SetPinsAmountText(int pinsAmount)
        {
           _pinsAmountText.text = $"Pins: {pinsAmount}";
        }
        
        public void BlockChangePinsButton()
        {
            _changePinsButton.interactable = false;
        }
        
        public void UnblockChangePinsButton()
        {
            _changePinsButton.interactable = true;
        }
    }
}