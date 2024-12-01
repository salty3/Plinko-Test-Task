using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Game.Scripts.BetSystem;
using Game.Scripts.CurrencySystem;
using UnityEngine.Events;
using Zenject;

namespace Game.Scripts.Scenes.GameScene.UI.BottomPanel
{
    public class BottomPanelPresenter : IInitializable
    {
        private readonly BottomPanelView _view;
        private readonly IBetService _betService;
        private ICurrencyService _currencyService;

        // Should be observables
        public readonly UnityEvent PlayHighBetButtonClicked = new();
        public readonly UnityEvent PlayMediumBetButtonClicked = new();
        public readonly UnityEvent PlayLowBetButtonClicked = new();

        [Inject]
        public BottomPanelPresenter(BottomPanelView view, IBetService betService, ICurrencyService currencyService)
        {
            _view = view;
            _betService = betService;
            _currencyService = currencyService;
        }

        public void Initialize()
        {
            _betService.BetAmount
                .Subscribe(OnBetAmountChanged)
                .AddTo(_view.DestroyCancellationToken);
            
            _currencyService.UsdBalance
                .Subscribe(OnBalanceChanged)
                .AddTo(_view.DestroyCancellationToken);
            
            _view.AddBetButton.OnClickAsAsyncEnumerable(_view.DestroyCancellationToken)
                .Subscribe(_ => OnAddBetButtonClicked());
          
            _view.SubtractBetButton.OnClickAsAsyncEnumerable(_view.DestroyCancellationToken)
                .Subscribe(_ => OnSubtractBetButtonClicked());
            
            _view.PlayLowBetButton.OnClickAsAsyncEnumerable(_view.DestroyCancellationToken)
                .Subscribe(_ => PlayLowBetButtonClicked.Invoke());
            
            _view.PlayMediumBetButton.OnClickAsAsyncEnumerable(_view.DestroyCancellationToken)
                .Subscribe(_ => PlayMediumBetButtonClicked.Invoke());

            _view.PlayHighBetButton.OnClickAsAsyncEnumerable(_view.DestroyCancellationToken)
                .Subscribe(_ => PlayHighBetButtonClicked.Invoke());
            
        }

        private void OnBalanceChanged(decimal balance)
        {
            UpdateInteractions(balance, _betService.BetAmount.Value);
        }
        
        private void OnBetAmountChanged(decimal amount)
        {
            _view.SetBetAmountText($"{amount:C}");
            UpdateInteractions(_currencyService.UsdBalance.Value, amount);
        }
        
        private void UpdateInteractions(decimal currentBalance, decimal currentBet)
        {
            if (currentBalance < currentBet)
            {
                BlockInteractions();
            }
            else
            {
                UnblockInteractions();
            }
        }
        
        private void OnAddBetButtonClicked()
        {
            _betService.IncrementBet();
        }
        
        private void OnSubtractBetButtonClicked()
        {
            _betService.DecrementBet();
        }
        
        private void BlockInteractions()
        {
            _view.BlockPlayButtons();
        }
        
        private void UnblockInteractions()
        {
            _view.UnblockPlayButtons();
        }
    }
}