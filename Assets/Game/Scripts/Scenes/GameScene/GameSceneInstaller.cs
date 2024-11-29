using Game.Scripts.Scenes.GameScene.Behaviour;
using Zenject;

namespace Game.Scripts.Scenes.GameScene
{
    public class GameSceneInstaller : MonoInstaller
    {
        
        [Inject]
        private void Construct()
        {
           
        }
        
        public override void InstallBindings()
        {
            
            Container.BindInterfacesAndSelfTo<GameplayLoopStateManager>().AsSingle();
        }
    }
}