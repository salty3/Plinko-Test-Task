using Game.Scripts.TimerSystem;
using UnityEngine.Events;
using Zenject;

namespace Game.Scripts.Gameplay.InfoPanel
{
    public class InfoPanelPresenter : IInitializable
    {
        private readonly InfoPanelView _view;
        private readonly Timer _timer;

        public UnityEvent ShuffleButtonClicked => _view.ShuffleButton.onClick;
        
        [Inject]
        public InfoPanelPresenter(InfoPanelView view, Timer timer)
        {
            _view = view;
            _timer = timer;
        }
        
        void IInitializable.Initialize()
        {
            _timer.Updated += () => _view.SetElapsedTimeText(_timer.ElapsedTime.ToString(@"mm\:ss"));
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