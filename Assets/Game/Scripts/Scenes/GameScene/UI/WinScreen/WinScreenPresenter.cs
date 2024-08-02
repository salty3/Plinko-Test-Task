using System;
using UnityEngine.Events;
using Zenject;

namespace Game.Scripts.Scenes.GameScene.UI.WinScreen
{
    public class WinScreenPresenter : IInitializable
    {
        private readonly WinScreenView _view;
        
        public UnityEvent PlayNextLevelClicked => _view.PlayNextLevelClicked;
        public UnityEvent ToMainMenuClicked => _view.ToMainMenuClicked;
        
        
        [Inject]
        public WinScreenPresenter(WinScreenView view)
        {
            _view = view;
        }
        
        void IInitializable.Initialize()
        {
            Hide();
        }

        public void HidePlayNextLevelButton()
        {
            _view.HidePlayNextLevelButton();
        }
        
        public void SetPlayedTime(TimeSpan time)
        {
            _view.SetPlayedTime(time.ToString(@"mm\:ss"));
        }
        
        public void Show()
        {
            _view.Show();
        }
        
        public void Hide()
        {
            _view.Hide();
        }
    }
}