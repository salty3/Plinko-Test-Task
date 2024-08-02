using Game.Scripts.Gameplay;
using Game.Scripts.GameScene.Gameplay;
using Tools.Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.MenuScene
{
    public class SelectLevelScreenView : UIMonoBehaviour
    {
        [SerializeField] private GridLayoutGroup _gridLayoutGroup;

        [SerializeField] private Button _backButton;
        
        [SerializeField] private LevelButton _levelButtonPrefab;
        
        public Button BackButton => _backButton;
        
        public void Clear()
        {
            if (this == null)
            {
                return;
            }
            
            foreach (Transform child in _gridLayoutGroup.transform)
            {
                Destroy(child.gameObject);
            }
        }
        
        public LevelButton CreateLevelButton(LevelData data)
        {
            var button = Instantiate(_levelButtonPrefab, _gridLayoutGroup.transform);
            button.SetIcon(data.LevelPreviewIcon);
            return button;
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