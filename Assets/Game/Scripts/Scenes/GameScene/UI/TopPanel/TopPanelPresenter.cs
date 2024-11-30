using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Game.Scripts.CurrencySystem;
using Zenject;

namespace Game.Scripts.Scenes.GameScene.UI.TopPanel
{
    public class TopPanelPresenter : IInitializable
    {
        private readonly TopPanelView _view;
        private readonly ICurrencyService _currencyService;

        [Inject]
        public TopPanelPresenter(TopPanelView view, ICurrencyService currencyService)
        {
            _view = view;
            _currencyService = currencyService;
        }
        
        public void Initialize()
        {
            _currencyService.UsdBalance
                .Subscribe(balance => _view.SetBalanceText($"{balance:C}"))
                .AddTo(_view.DestroyCancellationToken);
            //_view.SetBalanceText(_currencyService.UsdBalance.Value.ToString("C"));
            _view.ChangePinsButton
                .OnClickAsAsyncEnumerable(_view.DestroyCancellationToken)
                .Subscribe(_ => OnChangePinsButtonClicked());
            
            _view.BetHistoryButton
                .OnClickAsAsyncEnumerable(_view.DestroyCancellationToken)
                .Subscribe(_ => OnBetHistoryButtonClicked());
            
            _view.HowToPlayButton
                .OnClickAsAsyncEnumerable(_view.DestroyCancellationToken)
                .Subscribe(_ => OnHowToPlayButtonClicked());
        }
        
        private void OnChangePinsButtonClicked()
        {
            
        }
        
        private void OnBetHistoryButtonClicked()
        {
            
        }
        
        private void OnHowToPlayButtonClicked()
        {
            
        }
    }
}