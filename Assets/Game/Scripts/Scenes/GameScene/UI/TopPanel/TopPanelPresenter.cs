using System.Linq;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Game.Scripts.BetSystem;
using Game.Scripts.CurrencySystem;
using UnityEngine.Events;
using Zenject;

namespace Game.Scripts.Scenes.GameScene.UI.TopPanel
{
    public class TopPanelPresenter : IInitializable
    {
        private readonly TopPanelView _view;
        private readonly ICurrencyService _currencyService;
        private readonly PinRowsConfig _config;

        public readonly AsyncReactiveProperty<int> PinsAmount;


        private readonly int[] _pinRowsAmount;
        
        private int _currentPinRowIndex;

        [Inject]
        public TopPanelPresenter(TopPanelView view, ICurrencyService currencyService, PinRowsConfig config)
        {
            _view = view;
            _currencyService = currencyService;
            _pinRowsAmount = config.PinRowsAmount.ToArray();
            PinsAmount = new AsyncReactiveProperty<int>(_pinRowsAmount[0]);
        }
        
        public void Initialize()
        {
            _currencyService.UsdBalance
                .Subscribe(balance => _view.SetBalanceText($"{balance:C}"))
                .AddTo(_view.DestroyCancellationToken);
            _view.ChangePinsButton
                .OnClickAsAsyncEnumerable(_view.DestroyCancellationToken)
                .Subscribe(_ => OnChangePinsButtonClicked());
            
            _view.SetPinsAmountText(_pinRowsAmount[_currentPinRowIndex]);
        }
        
        public void BlockInteractions()
        {
            _view.BlockChangePinsButton();
        }
        
        public void UnblockInteractions()
        {
            _view.UnblockChangePinsButton();
        }
        
        private void OnChangePinsButtonClicked()
        {
            _currentPinRowIndex++;
            if (_currentPinRowIndex >= _pinRowsAmount.Length)
            {
                _currentPinRowIndex = 0;
            }
            int currentPinsAmount = _pinRowsAmount[_currentPinRowIndex];
            _view.SetPinsAmountText(currentPinsAmount);
            PinsAmount.Value = currentPinsAmount;
        }
    }
}