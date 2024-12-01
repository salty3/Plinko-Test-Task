using Tools.Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Scenes.GameScene.UI.AddBalancePopUp
{
    public class AddBalancePopUpView : UIMonoBehaviour
    {
        [SerializeField] private Button _addBalanceButton;
        
        public Button AddBalanceButton => _addBalanceButton;
        
        public void Show()
        {
            gameObject.SetActive(true);
        }
        
        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
