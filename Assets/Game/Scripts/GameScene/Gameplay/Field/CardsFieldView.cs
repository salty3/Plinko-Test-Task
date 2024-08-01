using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.Scripts.Gameplay.Card;
using Tools.Runtime;
using UnityEngine;
using Utility.SLayout;

namespace Game.Scripts.Gameplay
{
    public class CardsFieldView : UIMonoBehaviour
    {
        [SerializeField] private Vector2 _referenceFieldSize;
        [SerializeField] private Vector2 _referenceCellSize;
        
        [SerializeField] private SGridLayoutGroup _gridParent;
        
        [SerializeField] private CardView _cardPrefab;
        
        [SerializeField] private RectTransform _completionBoard;
        [SerializeField] private RectTransform _completionMoveToPoint;

        public float GridMoveDuration => _gridParent.moveDuration;
        private class CardViewPair
        {
            public CardView CardView1 { get; set; }
            public CardView CardView2 { get; set; }
        };
        
        private readonly Dictionary<string, CardViewPair> _cards = new();

        public void Initialize()
        {
            var sizeMultiplier = RectTransform.sizeDelta / _referenceFieldSize;
            var cellSize = _referenceCellSize * sizeMultiplier;
            _gridParent.cellSize = cellSize;
        }
        
        public CardView CreateCardView(CardData cardData, Sprite backIcon)
        {
            var cardView = Instantiate(_cardPrefab, _gridParent.transform);
            cardView.SetFrontIcon(cardData.Icon);
            cardView.SetBackIcon(backIcon);
            if (_cards.ContainsKey(cardData.ID))
            {
                _cards[cardData.ID].CardView2 = cardView;
            } 
            else
            {
                _cards[cardData.ID] = new CardViewPair {CardView1 = cardView};
            }
            return cardView;
        }

        public async UniTask ShowCompletion(string id)
        {
            var tuple = _cards[id];
            tuple.CardView1.SetAsCompleted();
            tuple.CardView2.SetAsCompleted();
            tuple.CardView1.CardsContainer.SetParent(_completionBoard);
            tuple.CardView2.CardsContainer.SetParent(_completionBoard);
            await UniTask.WhenAll(
                CompletionAnimation(tuple.CardView1.CardsContainer),
                CompletionAnimation(tuple.CardView2.CardsContainer)
            );
        }

        private async UniTask CompletionAnimation(RectTransform rectT)
        {
            await DOTween.Sequence()
                .Append(rectT.DOLocalMove(Vector3.zero, 0.3f))
                .Append(rectT.DOScale(1.2f, 0.3f))
                .Append(rectT.DOMove(_completionMoveToPoint.transform.position, 0.3f))
                .Append(rectT.DOScale(0, 0.3f));
        }
    }
}