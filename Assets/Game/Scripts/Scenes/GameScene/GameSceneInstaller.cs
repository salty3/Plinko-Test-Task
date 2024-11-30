using Game.Scripts.Scenes.GameScene.Behaviour;
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
        [SerializeField] private PinsRenderer _pinsRenderer;

        
        [Inject]
        private void Construct()
        {
           
        }
        
        public override void InstallBindings()
        {
            Container.BindInstance(_topPanelView).AsSingle();
            Container.BindInstance(_bottomPanelView).AsSingle();
            Container.BindInstance(_pinsRenderer);

            
            Container.BindInterfacesAndSelfTo<TopPanelPresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<BottomPanelPresenter>().AsSingle();
            
            
            Container.BindInterfacesAndSelfTo<GameplayLoopStateManager>().AsSingle();
        }
    }
}