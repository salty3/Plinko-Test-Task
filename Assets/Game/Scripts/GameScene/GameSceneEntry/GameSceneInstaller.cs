using Game.Scripts.FieldSystem;
using Game.Scripts.Gameplay;
using Game.Scripts.Gameplay.InfoPanel;
using Game.Scripts.GameScene.Gameplay.Behaviour;
using Game.Scripts.GameScene.Gameplay.LoseScreen;
using Game.Scripts.GameScene.Gameplay.WinScreen;
using Game.Scripts.TimerSystem;
using UnityEngine;
using Zenject;

namespace Game.Scripts.GameScene.GameSceneEntry
{
    public class GameSceneInstaller : MonoInstaller
    {
        [SerializeField] private InfoPanelView _infoPanelView;
        [SerializeField] private CardsFieldView _cardsFieldView;
        [SerializeField] private WinScreenView _winScreenView;
        [SerializeField] private LoseScreenView _loseScreenView;

        private ITimerService _timerService;
        private IReadOnlyLevelEntity _levelEntity;
        
        [Inject]
        private void Construct(ITimerService timerService, IReadOnlyLevelEntity levelEntity)
        {
            _timerService = timerService;
            _levelEntity = levelEntity;
        }
        
        public override void InstallBindings()
        {
            Container.BindInstance(_timerService.CreateTimer(_levelEntity.ElapsedTime));
            
            Container.BindInstance(_infoPanelView).WhenInjectedInto<InfoPanelPresenter>();
            Container.BindInstance(_cardsFieldView).WhenInjectedInto<CardsFieldPresenter>();
            Container.BindInstance(_winScreenView).WhenInjectedInto<WinScreenPresenter>();
            Container.BindInstance(_loseScreenView).WhenInjectedInto<LoseScreenPresenter>();
            
            Container.BindInterfacesAndSelfTo<CardsFieldPresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<InfoPanelPresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<WinScreenPresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<LoseScreenPresenter>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<GameplayLoopStateManager>().AsSingle();
        }
    }
}