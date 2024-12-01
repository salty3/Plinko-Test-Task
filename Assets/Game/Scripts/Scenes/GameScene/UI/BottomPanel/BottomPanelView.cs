using TMPro;
using Tools.Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Scenes.GameScene.UI.BottomPanel
{
    public class BottomPanelView : UIMonoBehaviour
    {
        [SerializeField] private TMP_Text _betAmountText;
        [SerializeField] private Button _subtractBetButton;
        [SerializeField] private Button _addBetButton;
        [SerializeField] private Button _playLowBetButton;
        [SerializeField] private Button _playMediumBetButton;
        [SerializeField] private Button _playHighBetButton;
        
        public Button SubtractBetButton => _subtractBetButton;
        public Button AddBetButton => _addBetButton;
        
        public Button PlayLowBetButton => _playLowBetButton;
        public Button PlayMediumBetButton => _playMediumBetButton;
        public Button PlayHighBetButton => _playHighBetButton;
        
        public void SetBetAmountText(string betAmount)
        {
            _betAmountText.text = betAmount;
        }
        
        public void BlockPlayButtons()
        {
            _playLowBetButton.interactable = false;
            _playMediumBetButton.interactable = false;
            _playHighBetButton.interactable = false;
        }
        
        public void UnblockPlayButtons()
        {
            _playLowBetButton.interactable = true;
            _playMediumBetButton.interactable = true;
            _playHighBetButton.interactable = true;
        }
    }
}