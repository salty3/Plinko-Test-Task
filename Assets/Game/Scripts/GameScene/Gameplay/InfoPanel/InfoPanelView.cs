using TMPro;
using Tools.Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Gameplay.InfoPanel
{
    public class InfoPanelView : UIMonoBehaviour
    {
        [SerializeField] private TMP_Text _matchCountText;
        [SerializeField] private TMP_Text _mismatchCountText;
        [SerializeField] private TMP_Text _elapsedTimeText;
        [SerializeField] private Button _shuffleButton;
        
        public Button ShuffleButton => _shuffleButton;
        
        public void SetElapsedTimeText(string time)
        {
            _elapsedTimeText.text = time;
        }
        
        public void SetMatchCount(int count)
        {
            _matchCountText.text = count.ToString();
        }
        
        public void SetMismatchCount(int count)
        {
            _mismatchCountText.text = count.ToString();
        }
    }
}