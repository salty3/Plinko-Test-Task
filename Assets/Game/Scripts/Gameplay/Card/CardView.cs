using Cysharp.Threading.Tasks;
using DG.Tweening;
using Tools.Runtime;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Scripts.Gameplay.Card
{
    public class CardView : UIMonoBehaviour, IPointerClickHandler
    {
        private enum CardSide
        {
            Front,
            Back
        }
        
        [SerializeField] private RectTransform _cardsContainer;
        [SerializeField] private Image _frontImage;
        [SerializeField] private Image _backImage;
        
        public UnityEvent OnClick { get; } = new();

        public async UniTask Select()
        {
            await DOTween.Sequence()
                .Append(_cardsContainer.DOScale(1.2f, 0.2f))
                .Append(_cardsContainer.DORotate(Vector3.up * 90, 0.4f))
                .AppendCallback(() => ChangeSide(CardSide.Front))
                .Append(_cardsContainer.DORotate(Vector3.up * 0, 0.4f));
        }
        
        public async UniTask Deselect()
        {
            await DOTween.Sequence()
                .Append(_cardsContainer.DORotate(Vector3.up * 90, 0.4f))
                .AppendCallback(() => ChangeSide(CardSide.Back))
                .Append(_cardsContainer.DORotate(Vector3.up * 0, 0.4f))
                .Append(_cardsContainer.DOScale(1f, 0.2f));
        }
        
        private void ChangeSide(CardSide side)
        {
            bool isFront = side == CardSide.Front;
            _frontImage.gameObject.SetActive(isFront);
            _backImage.gameObject.SetActive(!isFront);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClick.Invoke();
        }
    }
}