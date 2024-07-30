using Cysharp.Threading.Tasks;
using UnityEngine.Events;

namespace Game.Scripts.Gameplay.Card
{
    public class CardPresenter
    {
        public UnityEvent OnSelect { get; } = new();
        public UnityEvent OnDeselect { get; } = new();
        
        private readonly CardView _view;

        private bool _isSelected;
        
        public CardPresenter(CardView view)
        {
            _view = view;
            _view.OnClick.AddListener(OnClick);
        }
        
        private void Select()
        {
            _isSelected = true;
            _view.Select().Forget();
            OnSelect.Invoke();
        }
        
        private void Deselect()
        {
            _isSelected = false;
            _view.Deselect().Forget();
            OnDeselect.Invoke();
        }
        
        private void OnClick()
        {
            if (_isSelected)
            {
                Deselect();
            }
            else
            {
                Select();
            }
        }
    }
}