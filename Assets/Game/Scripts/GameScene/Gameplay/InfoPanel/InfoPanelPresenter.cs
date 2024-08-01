using UnityEngine.Events;

namespace Game.Scripts.Gameplay.InfoPanel
{
    public class InfoPanelPresenter
    {
        private readonly InfoPanelView _view;
        
        public UnityEvent ShuffleButtonClicked => _view.ShuffleButton.onClick;
        
        public InfoPanelPresenter(InfoPanelView view)
        {
            _view = view;
        }
        
        public void SetMatchCount(int count)
        {
            _view.SetMatchCount(count);
        }
        
        public void SetMismatchCount(int count)
        {
            _view.SetMismatchCount(count);
        }
    }
}