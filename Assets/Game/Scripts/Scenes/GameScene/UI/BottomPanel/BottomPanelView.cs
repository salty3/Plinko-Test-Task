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
        [SerializeField] private Button _chooseBetButton;
        [SerializeField] private Button _addBetButton;
        [SerializeField] private Button _autoBetButton;
        [SerializeField] private Button _playLowBetButton;
        [SerializeField] private Button _playMediumBetButton;
        [SerializeField] private Button _playHighBetButton;
        
        public Button SubtractBetButton => _subtractBetButton;
        public Button ChooseBetButton => _chooseBetButton;
        public Button AddBetButton => _addBetButton;
        public Button AutoBetButton => _autoBetButton;
            
        public Button PlayLowBetButton => _playLowBetButton;
        public Button PlayMediumBetButton => _playMediumBetButton;
        public Button PlayHighBetButton => _playHighBetButton;
        
        public void SetBetAmountText(string betAmount)
        {
            _betAmountText.text = betAmount;
        }
    }
}