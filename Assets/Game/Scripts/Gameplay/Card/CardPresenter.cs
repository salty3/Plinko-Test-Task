using System;
using Cysharp.Threading.Tasks;
using UnityEngine.Events;

namespace Game.Scripts.Gameplay.Card
{
    public class CardPresenter
    {
        public string ID { get; }
        
        public UnityEvent OnSelect { get; } = new();
        public UnityEvent OnDeselect { get; } = new();
        
        private readonly CardView _view;

        private bool _isSelected;
        
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
            _isSelected = true;
            await _view.Select();
            OnSelect.Invoke();
        }

        public async UniTask Deselect()
        {
            _isSelected = false;
            await _view.Deselect();
            OnDeselect.Invoke();
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