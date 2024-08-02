using Cysharp.Threading.Tasks;
using Game.Scripts.LevelsSystem.Card;
using Tools.Runtime;
using UnityEngine;
using Utility.SLayout;

namespace Game.Scripts.LevelsSystem.Field
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

        protected override async void Start()
        {
            // Waiting for rectT updates :(
            await UniTask.DelayFrame(2);
            var sizeMultiplier = RectTransform.rect.size / _referenceFieldSize;
            var cellSize = _referenceCellSize * sizeMultiplier;
            _gridParent.cellSize = cellSize;
        }
        
        public CardView CreateCardView(CardData cardData, Sprite backIcon)
        {
            var cardView = Instantiate(_cardPrefab, _gridParent.transform);
            cardView.RectTransform.localPosition = Vector3.zero;
            cardView.SetFrontIcon(cardData.Icon);
            cardView.SetBackIcon(backIcon);
            cardView.SetAnimationTransforms(_completionBoard, _completionMoveToPoint);
            return cardView;
        }
    }
}