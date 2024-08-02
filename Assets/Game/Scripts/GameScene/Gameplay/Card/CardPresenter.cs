using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Scripts.Gameplay.Card
{
    public class CardPresenter
    {
        public string ID { get; }
        
        //Unity Events because they can be easily awaited
        public UnityEvent Clicked { get; } = new();
        
        private readonly CardView _view;

        private bool _isSelected;
        private bool _isBlocked;
        
        public CardPresenter(string id, CardView view)
        {
            ID = id;
            _view = view;
            _view.OnClick.AddListener(() => OnClick().Forget());
        }
        
        public void SetOrderIndex(int index)
        {
            _view.RectTransform.SetSiblingIndex(index);
        }
        
        public async UniTask Select()
        {
            _isSelected = true;
            await _view.Select();
        }

        public async UniTask Deselect()
        {
            _isSelected = false;
            await _view.Deselect();
        }
        
        public void BlockInteraction()
        {
            _isBlocked = true;
        }
        
        public void UnblockInteraction()
        {
            _isBlocked = false;
        }
        
        public void SetAsMatched(bool hide = false)
        {
            _isBlocked = true;
            if (hide)
            {
                _view.Hide();
            }
        }
        
        public UniTask PlayMatchedAnimation()
        {
            return _view.PlayMatchedAnimation();
        }
        
        private async UniTask OnClick()
        {
            if (_isBlocked)
            {
                return;
            }
            
            if (_isSelected)
            {
                await Deselect();
            }
            else
            {
                await Select();
            }
            
            Clicked.Invoke();
        }
    }
}