using Game.Scripts.Gameplay;
using Game.Scripts.Gameplay.InfoPanel;
using Game.Scripts.GameScene.Gameplay.Behaviour;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Scripts
{
    public class GameSceneInstaller : MonoInstaller
    {
        [SerializeField] private InfoPanelView _infoPanelView;
        [SerializeField] private CardsFieldView _cardsFieldView;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_infoPanelView).WhenInjectedInto<InfoPanelPresenter>();
            Container.BindInstance(_cardsFieldView).WhenInjectedInto<CardsFieldPresenter>();
            
            Container.BindInterfacesAndSelfTo<CardsFieldPresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<InfoPanelPresenter>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<GameplayLoopStateManager>().AsSingle();
        }
    }
}