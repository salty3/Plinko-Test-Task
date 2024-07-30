using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Scripts.Gameplay.Card;
using Tools.Runtime;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Game.Scripts.Gameplay
{
    public class CardsFieldPresenter : IInitializable
    {
        private readonly CardsFieldView _view;

        private CardPresenter _selectedPresenter;

        private HashSet<string> _completedPairs;

        private List<CardPresenter> _cardsOrder;

        private Dictionary<string, (CardPresenter, CardPresenter)> _cards;
        
        public UnityEvent Matched { get; } = new();
        public UnityEvent Mismatched { get; } = new();
        
        [Inject]
        public CardsFieldPresenter(CardsFieldView view)
        {
            _view = view;
            _completedPairs = new HashSet<string>();
            _cardsOrder = new List<CardPresenter>();
            _cards = new Dictionary<string, (CardPresenter, CardPresenter)>();
        }
        
        void IInitializable.Initialize()
        {
            _view.Initialize();
        }

        public void BlockInteraction()
        {
            foreach (var cardPresenter in _cardsOrder)
            {
                cardPresenter.BlockInteraction();
            }
        }
        
        public void UnblockInteraction()
        {
            foreach (var cardPresenter in _cardsOrder)
            {
                cardPresenter.UnblockInteraction();
            }
        }

        private void OnCardSelected(CardPresenter presenter)
        {
            if (_selectedPresenter == null)
            {
                _selectedPresenter = presenter;
                return;
            }
            
            if (presenter == _selectedPresenter)
            {
                _selectedPresenter = null;
                return;
            }
            
            if (_selectedPresenter.ID == presenter.ID)
            {
                _completedPairs.Add(presenter.ID);
                _selectedPresenter = null;
                Matched.Invoke();
                return;
            }

            _selectedPresenter.Deselect().Forget();
            presenter.Deselect().Forget();
            _selectedPresenter = null;
            Mismatched.Invoke();
        }
        
        public void SetLevel(LevelData levelData)
        {
            foreach (var cardData in levelData.Cards)
            {
                var cardPresenter1 = CreateCardPresenter(cardData, levelData.CardBack);
                var cardPresenter2 = CreateCardPresenter(cardData, levelData.CardBack);
                
                cardPresenter1.Deselect().Forget();
                cardPresenter2.Deselect().Forget();
                
                _cardsOrder.Add(cardPresenter1);
                _cardsOrder.Add(cardPresenter2);
                
                _cards[cardData.ID] = (cardPresenter1, cardPresenter2);
            }
        }
        
        private CardPresenter CreateCardPresenter(CardData cardData, Sprite backIcon)
        {
            var cardView = _view.CreateCardView(cardData, backIcon);
            var cardPresenter = new CardPresenter(cardData.ID, cardView);
            cardPresenter.Selected.AddListener(() => OnCardSelected(cardPresenter));
            return cardPresenter;
        }
        
        public void Shuffle()
        {
            _cardsOrder.Shuffle();
            for (var i = 0; i < _cardsOrder.Count; i++)
            {
                _cardsOrder[i].SetOrderIndex(i);
            }
            //flip face
            //flip back
        }
    }
}