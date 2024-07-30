using Game.Scripts.Gameplay.Card;
using Tools.Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Gameplay
{
    public class CardsFieldView : UIMonoBehaviour
    {
        [SerializeField] private Vector2 _referenceFieldSize;
        [SerializeField] private Vector2 _referenceCellSize;
        
        [SerializeField] private GridLayoutGroup _gridParent;
        
        [SerializeField] private CardView _cardPrefab;


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
            return cardView;
        }
    }
}