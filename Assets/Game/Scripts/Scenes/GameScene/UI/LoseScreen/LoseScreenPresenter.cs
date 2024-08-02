using UnityEngine.Events;
using Zenject;

namespace Game.Scripts.Scenes.GameScene.UI.LoseScreen
{
    public class LoseScreenPresenter : IInitializable
    {
        private readonly LoseScreenView _view;
        
        public UnityEvent RetryLevelClicked => _view.RetryLevelClicked;
        public UnityEvent ToMainMenuClicked => _view.ToMainMenuClicked;
        
        
        [Inject]
        public LoseScreenPresenter(LoseScreenView view)
        {
            _view = view;
        }
        
        void IInitializable.Initialize()
        {
            Hide();
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