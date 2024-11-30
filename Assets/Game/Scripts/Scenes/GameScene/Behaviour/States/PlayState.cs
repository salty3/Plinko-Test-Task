using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Game.Scripts.BetSystem;
using Game.Scripts.CurrencySystem;
using Game.Scripts.Scenes.GameScene.UI.BottomPanel;
using Game.Scripts.Scenes.GameScene.UI.TopPanel;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Scenes.GameScene.Behaviour.States
{
    public class PlayState : GameState
    {
        private readonly PlinkoCore _plinkoCore = new(12);

        private TopPanelPresenter _topPanelPresenter;
        private readonly BottomPanelPresenter _bottomPanelPresenter;

        private readonly PinsRenderer _pinsRenderer;

        private readonly IBetService _betService;
        private readonly ICurrencyService _currencyService;

        private CancellationTokenSource _stateCts;
        
        [Inject]
        public PlayState(TopPanelPresenter topPanelPresenter, BottomPanelPresenter bottomPanelPresenter, PinsRenderer pinsRenderer, ICurrencyService currencyService, IBetService betService)
        {
            _topPanelPresenter = topPanelPresenter;
            _bottomPanelPresenter = bottomPanelPresenter;
            _pinsRenderer = pinsRenderer;
            _currencyService = currencyService;
            _betService = betService;
        }
        
        public override void Initialize()
        {
            _stateCts = new CancellationTokenSource();
            
            _pinsRenderer.RenderBoard(12);

            _bottomPanelPresenter.PlayHighBetButtonClicked
                .OnInvokeAsAsyncEnumerable(_stateCts.Token)
                .Subscribe(_ => PlayBet(BetRisk.High));

            _bottomPanelPresenter.PlayMediumBetButtonClicked
                .OnInvokeAsAsyncEnumerable(_stateCts.Token)
                .Subscribe(_ => PlayBet(BetRisk.Medium));

            _bottomPanelPresenter.PlayLowBetButtonClicked
                .OnInvokeAsAsyncEnumerable(_stateCts.Token)
                .Subscribe(_ => PlayBet(BetRisk.Low));
        }

        public override void Dispose()
        {
            _stateCts.Cancel();
        }
        
        private async void PlayBet(BetRisk risk)
        {
            decimal betAmount = _betService.BetAmount.Value;
            _currencyService.SubtractUsd(betAmount);
            
            //Ideally all calculations should be on backend
            //At least we should add balance right after roll calculated (and save it) and update UI after animation ends
            var roll = _plinkoCore.CalculateRoll();
            await _pinsRenderer.PlayRollAnimation(roll);
            decimal multiplier = _betService.GetMultiplier(risk, roll.ResultHole);
            Debug.Log($"multiplier: {multiplier}, hole: {roll.ResultHole}");
            decimal balanceToAdd = betAmount * multiplier;

            _currencyService.AddUsd(balanceToAdd);
        }
    }
}