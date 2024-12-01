using Game.Scripts.Core;
using Game.Scripts.Scenes.GameScene.UI.AddBalancePopUp;
using Game.Scripts.Scenes.GameScene.UI.BottomPanel;
using Game.Scripts.Scenes.GameScene.UI.TopPanel;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Scenes.GameScene
{
    public class GameSceneInstaller : MonoInstaller
    {
        [SerializeField] private TopPanelView _topPanelView;
        [SerializeField] private BottomPanelView _bottomPanelView;
        [SerializeField] private AddBalancePopUpView _addBalancePopUpView;
        [SerializeField] private PinsRenderer _pinsRenderer;

        
        [Inject]
        private void Construct()
        {
           
        }
        
        public override void InstallBindings()
        {
            Container.BindInstance(_topPanelView);
            Container.BindInstance(_bottomPanelView);
            Container.BindInstance(_addBalancePopUpView);
            Container.BindInstance(_pinsRenderer);

            
            Container.BindInterfacesAndSelfTo<TopPanelPresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<BottomPanelPresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<AddBalancePopUpPresenter>().AsSingle();
            
            
            Container.BindInterfacesAndSelfTo<PlinkoCore>().AsSingle();
        }
    }
}