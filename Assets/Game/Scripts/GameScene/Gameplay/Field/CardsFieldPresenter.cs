using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Game.Scripts.FieldSystem;
using Game.Scripts.Gameplay.Card;
using Game.Scripts.GameScene.Gameplay;
using Game.Scripts.GameScene.Gameplay.Card;
using Game.Scripts.PlayerSystem;
using Tools.Runtime;
using UnityEngine;
using UnityEngine.Events;
using Zenject;
using Random = UnityEngine.Random;

namespace Game.Scripts.Gameplay
{
    public class CardsFieldPresenter : IInitializable
    {
        private readonly CardsFieldView _view;

        private CardPresenter _selectedPresenter;

        private List<CardPresenter> _cardPresenters;
        
        public UnityEvent<string> Matched { get; } = new();
        public UnityEvent Mismatched { get; } = new();

        private IReadOnlyCardsFieldEntity _cardsFieldEntity;

        private ILevelsService _levelsService;

        private IReadOnlyLevelEntity _levelEntity;
        
        [Inject]
        public CardsFieldPresenter(CardsFieldView view, ILevelsService levelsService, IReadOnlyLevelEntity levelEntity)
        {
            _view = view;
            _levelsService = levelsService;
            _levelEntity = levelEntity;
        }
        
        void IInitializable.Initialize()
        {
            _view.Initialize();
        }

        public void BlockInteraction()
        {
            foreach (var cardPresenter in _cardPresenters)
            {
                cardPresenter.BlockInteraction();
            }
        }
        
        public void UnblockInteraction()
        {
            foreach (var cardPresenter in _cardPresenters)
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
        
        public async UniTask SetField(IReadOnlyCardsFieldEntity cardsFieldEntity)
        {
            _cardsFieldEntity = cardsFieldEntity;
            _cardPresenters = cardsFieldEntity.Cards.ToList().ConvertAll(CreateCardPresenter);
            await UniTask.Delay(TimeSpan.FromSeconds(_view.GridMoveDuration));
        }
        
        private CardPresenter CreateCardPresenter(IReadOnlyCardEntity cardEntity)
        {
            var levelData = _levelsService.GetLevelData(_levelEntity.LevelIndex);
            var cardData = levelData.Cards.FirstOrDefault(cardData => cardData.ID == cardEntity.ID);
            var cardView = _view.CreateCardView(cardData, levelData.CardBack);
            var cardPresenter = new CardPresenter(cardEntity.ID, cardView);
            cardPresenter.Deselect().Forget();
            cardPresenter.Clicked.AddListener(() => OnCardSelected(cardPresenter));
            if (cardEntity.IsMatched)
            {
                cardPresenter.SetAsMatched(true);
            }
            return cardPresenter;
        }
        
        public async UniTask Shuffle()
        {
            int seed = Random.Range(int.MinValue, int.MaxValue);
            _cardPresenters.Shuffle(seed);
            _levelsService.ShuffleField(_levelEntity.LevelIndex, seed);

            for (var i = 0; i < _cardPresenters.Count; i++)
            {
                _cardPresenters[i].SetOrderIndex(i);
            }

            await UniTask.Delay(TimeSpan.FromSeconds(_view.GridMoveDuration));
        }

        public async UniTask ShowCardsFor(TimeSpan time)
        {
            await UniTask.WhenAll(_cardPresenters.Select(cardPresenter => cardPresenter.Select()));
            await UniTask.Delay(time);
            await UniTask.WhenAll(_cardPresenters.Select(cardPresenter => cardPresenter.Deselect()));
        }
        
        public void CompletePair(string id)
        {
            var presenters = _cardPresenters.FindAll(p => p.ID == id);
            foreach (var cardPresenter in presenters)
            {
                cardPresenter.SetAsMatched();
                cardPresenter.PlayMatchedAnimation().Forget();
            }
        }
    }
}