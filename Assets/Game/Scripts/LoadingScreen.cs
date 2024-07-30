using Cysharp.Threading.Tasks;
using DG.Tweening;
using Tools.Runtime;
using Tools.SceneManagement.Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts
{
    public class LoadingScreen : CachedMonoBehaviour, ILoadingScreen
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Slider _progressSlider;

        private const float FADE_DURATION = 0.2f;
        
        public async UniTask Open()
        {
            GameObject.SetActive(true);
            _canvasGroup.alpha = 0f;
            await _canvasGroup.DOFade(1f, FADE_DURATION);
        }

        public async UniTask Close()
        {
            await _canvasGroup.DOFade(0f, FADE_DURATION);
            GameObject.SetActive(false);
        }

        public void SetProgress(float progress)
        {
            _progressSlider.DOValue(progress, 0.2f);
        }
    }
}