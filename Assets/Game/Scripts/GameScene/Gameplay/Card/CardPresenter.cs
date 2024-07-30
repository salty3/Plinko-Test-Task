using System;
using Cysharp.Threading.Tasks;
using UnityEngine.Events;

namespace Game.Scripts.Gameplay.Card
{
    public class CardPresenter
    {
        public string ID { get; }
        
        //Unity Events because they can be easily awaited
        public UnityEvent Selected { get; } = new();
        public UnityEvent Deselected { get; } = new();
        
        private readonly CardView _view;

        private bool _isSelected;
        private bool _isBlocked;
        
        public CardPresenter(string id, CardView view)
        {
            ID = id;
            _view = view;
            _view.OnClick.AddListener(OnClick);
        }
        
        public void SetOrderIndex(int index)
        {
            _view.RectTransform.SetSiblingIndex(index);
        }
        
        private async UniTask Select()
        {
            if (_isBlocked)
            {
                return;
            }
            
            _isSelected = true;
            await _view.Select();
            Selected.Invoke();
        }

        public async UniTask Deselect()
        {
            if (_isBlocked)
            {
                return;
            }
            
            _isSelected = false;
            await _view.Deselect();
            Deselected.Invoke();
        }
        
        public void BlockInteraction()
        {
            _isBlocked = true;
        }
        
        public void UnblockInteraction()
        {
            _isBlocked = false;
        }
        
        private void OnClick()
        {
            if (_isSelected)
            {
                Deselect().Forget();
            }
            else
            {
                Select().Forget();
            }
        }
    }
}