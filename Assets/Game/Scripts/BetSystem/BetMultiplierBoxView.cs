using DG.Tweening;
using Gilzoide.RoundedCorners;
using TMPro;
using Tools.Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.BetSystem
{
    public class BetMultiplierBoxView : UIMonoBehaviour
    {
        [SerializeField] private RoundedRect _graphic;
        [SerializeField] private TMP_Text _betMultiplierText;
        [SerializeField] private RectTransform _tintGraphic;

        private Tweener _tweener;
        
        public void SetBetMultiplier(decimal multiplier)
        {
            /*if (multiplier < 1.0m)
            {
                //not works for some reason
                //_graphic.color = new Color(190, 190, 190);
            }*/
            
            _tintGraphic.gameObject.SetActive(multiplier < 1.0m);
            
            _betMultiplierText.text = $"{multiplier}";
        }

        public void PlayAnimation()
        {
            _tweener?.Kill();
            _tweener = Transform.DOScale(0.85f, 0.1f).SetLoops(2, LoopType.Yoyo);
        }
    }
}
