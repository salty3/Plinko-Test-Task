using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Game.Scripts.BetSystem;
using Zenject;

namespace Game.Scripts.Scenes.GameScene.UI.BottomPanel
{
    public class BottomPanelPresenter : IInitializable
    {
        private readonly BottomPanelView _view;
        private readonly IBetService _betService;

        [Inject]
        public BottomPanelPresenter(BottomPanelView view, IBetService betService)
        {
            _view = view;
            _betService = betService;
        }

        public void Initialize()
        {
            _betService.BetAmount
                .Subscribe(amount => _view.SetBetAmountText($"{amount:C}"))
                .AddTo(_view.DestroyCancellationToken);
            
            _view.AddBetButton.OnClickAsAsyncEnumerable(_view.DestroyCancellationToken)
                .Subscribe(_ => OnAddBetButtonClicked());
            
            _view.ChooseBetButton.OnClickAsAsyncEnumerable(_view.DestroyCancellationToken)
                .Subscribe(_ => OnChooseBetButtonClicked());
            
            _view.SubtractBetButton.OnClickAsAsyncEnumerable(_view.DestroyCancellationToken)
                .Subscribe(_ => OnSubtractBetButtonClicked());
            
            _view.AutoBetButton.OnClickAsAsyncEnumerable(_view.DestroyCancellationToken)
                .Subscribe(_ => OnAutoBetButtonClicked());
            
            _view.PlayLowBetButton.OnClickAsAsyncEnumerable(_view.DestroyCancellationToken)
                .Subscribe(_ => OnPlayLowBetButtonClicked());
            
            _view.PlayMediumBetButton.OnClickAsAsyncEnumerable(_view.DestroyCancellationToken)
                .Subscribe(_ => OnPlayMediumBetButtonClicked());

            _view.PlayHighBetButton.OnClickAsAsyncEnumerable(_view.DestroyCancellationToken)
                .Subscribe(_ => OnPlayHighBetButtonClicked());
            
        }
        
        private void OnAddBetButtonClicked()
        {
            
        }
        
        private void OnChooseBetButtonClicked()
        {
            
        }
        
        private void OnSubtractBetButtonClicked()
        {
            
        }
        
        private void OnAutoBetButtonClicked()
        {
            
        }
        
        private void OnPlayLowBetButtonClicked()
        {
            
        }
        
        private void OnPlayMediumBetButtonClicked()
        {
            
        }
        
        private void OnPlayHighBetButtonClicked()
        {
            
        }
    }
}