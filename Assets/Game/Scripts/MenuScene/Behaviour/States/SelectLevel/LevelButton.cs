using Tools.Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.MenuScene
{
    public class LevelButton : UIMonoBehaviour
    {
        [SerializeField] private Image _preview;
        [SerializeField] private Button _button;
        
        public Button Button => _button;
        
        public void SetIcon(Sprite icon)
        {
            _preview.sprite = icon;
        }
        
        public void SetAsCompleted()
        {
            _button.interactable = false;
        }
    }
}