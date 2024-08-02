using System;
using TMPro;
using Tools.Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Scenes.MenuScene.Behaviour.States.SelectLevel
{
    public class LevelButton : UIMonoBehaviour
    {
        [SerializeField] private Image _preview;
        [SerializeField] private Image _border;
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _timeText;
        [SerializeField] private Transform _completedIcon;
        
        public Button Button => _button;
        
        public void SetIcon(Sprite icon)
        {
            _preview.sprite = icon;
        }
        
        public void SetAsLocked()
        {
            _button.interactable = false;
            _border.color = Color.gray;
            _preview.color = Color.gray;
        }
        
        public void SetAsCompleted(TimeSpan elapsedTime)
        {
            _timeText.gameObject.SetActive(true);
            _timeText.text = elapsedTime.ToString(@"mm\:ss");
            
            _completedIcon.gameObject.SetActive(true);

            SetAsLocked();
        }
    }
}