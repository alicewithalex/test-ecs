using DG.Tweening;
using System;
using UnityEngine;

namespace alicewithalex
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class UIScreen : MonoBehaviour
    {
        private const float FADE_TIME = 0.5f;
        private const Ease EASING = Ease.OutQuad;

        [Tooltip("For Editor purposes only")]
        [SerializeField] private ScreenType _screenType;

        [Space(8)]
        [SerializeField] CanvasGroup _canvasGroup;

        public ScreenType Type => _screenType;

        private UILayer _layer;

        public event Action<UIScreen> OnScreenShowed;
        public event Action<UIScreen> OnScreenHided;

        public void Initialize(UILayer layer)
        {
            if (_layer != null) return;

            _layer = layer;
        }

        public void Show(float duration = FADE_TIME,
            Ease easing = EASING)
        {
            if (DOTween.IsTweening(_canvasGroup))
                DOTween.Kill(_canvasGroup);

            if (_layer && !_layer.Active)
            {
                _layer.Active = true;
            }

            gameObject.SetActive(true);

            if (duration <= 0)
            {
                _canvasGroup.alpha = 1f;
                _canvasGroup.interactable = _canvasGroup.blocksRaycasts = true;
                OnScreenShowed?.Invoke(this);
            }
            else
            {
                _canvasGroup.blocksRaycasts = true;
                _canvasGroup.DOFade(1f, duration)
                    .SetUpdate(true)
                    .SetEase(easing)
                    .OnComplete(() =>
                    {
                        _canvasGroup.interactable = true;
                        OnScreenShowed?.Invoke(this);
                    });
            }
        }

        public void Hide(float duration = FADE_TIME,
            Ease easing = EASING)
        {
            if (DOTween.IsTweening(_canvasGroup))
                DOTween.Kill(_canvasGroup);


            if (duration <= 0)
            {
                gameObject.SetActive(false);

                _canvasGroup.alpha = 0f;
                _canvasGroup.interactable = _canvasGroup.blocksRaycasts = false;
                OnScreenHided?.Invoke(this);
            }
            else
            {
                _canvasGroup.interactable = false;
                _canvasGroup.DOFade(0f, duration)
                    .SetUpdate(true)
                    .SetEase(easing)
                    .OnComplete(() =>
                    {
                        _canvasGroup.blocksRaycasts = false;
                        gameObject.SetActive(false);
                        OnScreenHided?.Invoke(this);
                    });
            }
        }

        private void Reset()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }
    }
}