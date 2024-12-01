using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Game.Scripts.BetSystem;
using Game.Scripts.CurrencySystem;
using Game.Scripts.Scenes.GameScene.UI.BottomPanel;
using Game.Scripts.Scenes.GameScene.UI.TopPanel;
using Zenject;

namespace Game.Scripts.Core
{
    public class PlinkoCore : IDisposable
    {
        public AsyncReactiveProperty<int> ActiveRolls { get; } = new(0);
        
        private PlinkoLogic _plinkoLogic;

        private readonly TopPanelPresenter _topPanelPresenter;
        private readonly BottomPanelPresenter _bottomPanelPresenter;

        private readonly PinsRenderer _pinsRenderer;

        private readonly IBetService _betService;
        private readonly ICurrencyService _currencyService;

        private CancellationTokenSource _cts;

        private readonly WinValuesConfig _winValuesConfig;
        
        [Inject]
        public PlinkoCore(TopPanelPresenter topPanelPresenter, BottomPanelPresenter bottomPanelPresenter, PinsRenderer pinsRenderer, ICurrencyService currencyService, IBetService betService, WinValuesConfig winValuesConfig)
        {
            _topPanelPresenter = topPanelPresenter;
            _bottomPanelPresenter = bottomPanelPresenter;
            _pinsRenderer = pinsRenderer;
            _currencyService = currencyService;
            _betService = betService;
            _winValuesConfig = winValuesConfig;
        }
        
        public void Initialize()
        {
            _cts = new CancellationTokenSource();
            
            _topPanelPresenter.PinsAmount
                .Subscribe(ChangePinRows)
                .AddTo(_cts.Token);

            _bottomPanelPresenter.PlayHighBetButtonClicked
                .OnInvokeAsAsyncEnumerable(_cts.Token)
                .Subscribe(_ => PlayBet(BetRisk.High).Forget());

            _bottomPanelPresenter.PlayMediumBetButtonClicked
                .OnInvokeAsAsyncEnumerable(_cts.Token)
                .Subscribe(_ => PlayBet(BetRisk.Medium).Forget());

            _bottomPanelPresenter.PlayLowBetButtonClicked
                .OnInvokeAsAsyncEnumerable(_cts.Token)
                .Subscribe(_ => PlayBet(BetRisk.Low).Forget());
            
            ChangePinRows(_topPanelPresenter.PinsAmount.Value);
        }

        private void ChangePinRows(int rowsCount)
        {
            _plinkoLogic = new PlinkoLogic(rowsCount);
            _pinsRenderer.RenderAll(rowsCount, _winValuesConfig);
        }

        public void Dispose()
        {
            _cts.Cancel();
        }
        
        private async UniTaskVoid PlayBet(BetRisk risk)
        {
            decimal betAmount = _betService.BetAmount.Value;
            if (!_currencyService.TrySubtractUsd(betAmount))
            {
                return;
            }

            ActiveRolls.Value++;
            CheckLocking();
            //Ideally all calculations should be on backend
            //At least we should add balance right after roll calculated (and save it) and update UI after animation ends
            var roll = _plinkoLogic.CalculateRoll();
            await _pinsRenderer.PlayRollAnimation(risk, roll);
            decimal multiplier = _betService.GetMultiplier(risk, roll.ResultHole);
            decimal balanceToAdd = betAmount * multiplier;

            _currencyService.AddUsd(balanceToAdd);
            ActiveRolls.Value--;
            CheckLocking();
        }
        
        private void CheckLocking()
        {
            if (ActiveRolls.Value > 0)
            {
                _topPanelPresenter.BlockInteractions();
            }
            else
            {
                _topPanelPresenter.UnblockInteractions();
            }
        }
    }
}