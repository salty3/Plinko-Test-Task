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
        
        public RectTransform CardsContainer => _cardsContainer;
        
        public UnityEvent OnClick { get; } = new();

        private Tween _tween;
        
        private bool _isCompleted;

        public void SetAsCompleted()
        {
            _isCompleted = true;
        }
        
        public void SetFrontIcon(Sprite icon)
        {
            _frontImage.sprite = icon;
        }
        
        public void SetBackIcon(Sprite back)
        {
            _backImage.sprite = back;
        }

        public async UniTask Select()
        {
            _tween?.Kill();
            _tween = DOTween.Sequence()
                //.Append(_cardsContainer.DOScale(1.2f, 0.1f))
                .Append(_cardsContainer.DORotate(Vector3.up * 90, 0.2f))
                .AppendCallback(() => ChangeSide(CardSide.Front))
                .Append(_cardsContainer.DORotate(Vector3.up * 0, 0.2f));
            await _tween;
        }
        
        public async UniTask Deselect()
        {
            _tween?.Kill();
            _tween = DOTween.Sequence()
                .Append(_cardsContainer.DORotate(Vector3.up * 90, 0.2f))
                .AppendCallback(() => ChangeSide(CardSide.Back))
                .Append(_cardsContainer.DORotate(Vector3.up * 0, 0.2f));
               // .Append(_cardsContainer.DOScale(1f, 0.1f));
            await _tween;
        }
        
        private void ChangeSide(CardSide side)
        {
            bool isFront = side == CardSide.Front;
            _frontImage.gameObject.SetActive(isFront);
            _backImage.gameObject.SetActive(!isFront);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_isCompleted)
            {
                return;
            }
            OnClick.Invoke();
        }
    }
}