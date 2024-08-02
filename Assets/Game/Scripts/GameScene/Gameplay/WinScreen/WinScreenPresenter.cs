using System;
using System.Linq;
using Game.Scripts.Gameplay;
using UnityEngine.Events;
using Zenject;

namespace Game.Scripts.GameScene.Gameplay.WinScreen
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

        public void SetScore(string scoreText)
        {
            _view.SetScore(scoreText);
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