using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Game.Scripts.BetSystem;
using Game.Scripts.Core;
using Game.Scripts.CurrencySystem;
using Zenject;

namespace Game.Scripts.Scenes.GameScene.UI.AddBalancePopUp
{
    public class AddBalancePopUpPresenter : IInitializable
    {
        private readonly ICurrencyService _currencyService;
        private readonly AddBalancePopUpView _view;
        private readonly PlinkoCore _plinkoCore;
        
        private readonly decimal _minBet;

        public AddBalancePopUpPresenter(ICurrencyService currencyService, AddBalancePopUpView view, BetsConfig betsConfig, PlinkoCore plinkoCore)
        {
            _currencyService = currencyService;
            _view = view;
            _plinkoCore = plinkoCore;
            _minBet = Convert.ToDecimal(betsConfig.BetPresets.First());
        }

        public void Initialize()
        {
            _view.Hide();
            
            _view.AddBalanceButton
                .OnClickAsAsyncEnumerable(_view.DestroyCancellationToken)
                .Subscribe(_ => _currencyService.AddUsd(1000));

            _currencyService.UsdBalance
                .Subscribe(balance => OnBalanceChanged(balance, _plinkoCore.ActiveRolls.Value))
                .AddTo(_view.DestroyCancellationToken);

            _plinkoCore.ActiveRolls
                .Subscribe(activeRolls => OnBalanceChanged(_currencyService.UsdBalance.Value, activeRolls))
                .AddTo(_view.DestroyCancellationToken);
        }
        
        private void OnBalanceChanged(decimal balance, int activeRolls)
        {
            if (balance <= _minBet && activeRolls == 0)
            {
                _view.Show();
            }
            else
            {
                _view.Hide();
            }
        }
    }
}