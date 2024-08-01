using System;
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

        private readonly List<CardPresenter> _cardsOrder;
        private readonly Dictionary<string, CardPresenterPair> _cards;
        
        public UnityEvent<string> Matched { get; } = new();
        public UnityEvent Mismatched { get; } = new();
        
        private class CardPresenterPair
        {
            public CardPresenter CardPresenter1 { get; set; }
            public CardPresenter CardPresenter2 { get; set; }
        }
        
        [Inject]
        public CardsFieldPresenter(CardsFieldView view)
        {
            _view = view;
            _cardsOrder = new List<CardPresenter>();
            _cards = new Dictionary<string, CardPresenterPair>();
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
                _selectedPresenter = null;
                Matched.Invoke(presenter.ID);
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
                
                _cards[cardData.ID] = new CardPresenterPair()
                {
                    CardPresenter1 = cardPresenter1,
                    CardPresenter2 = cardPresenter2
                };
            }
        }
        
        private CardPresenter CreateCardPresenter(CardData cardData, Sprite backIcon)
        {
            var cardView = _view.CreateCardView(cardData, backIcon);
            var cardPresenter = new CardPresenter(cardData.ID, cardView);
            cardPresenter.Clicked.AddListener(() => OnCardSelected(cardPresenter));
            return cardPresenter;
        }
        
        public async UniTask Shuffle()
        {
            _cardsOrder.Shuffle();
            for (var i = 0; i < _cardsOrder.Count; i++)
            {
                _cardsOrder[i].SetOrderIndex(i);
            }

            await UniTask.Delay(TimeSpan.FromSeconds(_view.GridMoveDuration));
        }

        public async UniTask ShowCardsFor(TimeSpan time)
        {
            await UniTask.WhenAll(_cardsOrder.Select(cardPresenter => cardPresenter.Select()));
            await UniTask.Delay(time);
            await UniTask.WhenAll(_cardsOrder.Select(cardPresenter => cardPresenter.Deselect()));
        }
        
        public void CompletePair(string id)
        {
            _view.ShowCompletion(id).Forget();
        }
    }
}