using System;
using Code.Modules.UiBaseModule.Enums;
using DG.Tweening;
using UnityEngine;

namespace Code.Modules.UiBaseModule.Mono
{
    public class UiAnimator : MonoBehaviour
    {
        [Header("Open")] 
        [SerializeField] private UiAnimationType _openType;
        [SerializeField] private float _openStartValue;
        [SerializeField] private float _openEndValue;
        [SerializeField] private float _openDuration;
        [SerializeField] private Ease _openEase;
        [SerializeField] private UiAnimationDirectionType _openDirectionType;
        [SerializeField] private string _openTriggerName;

        [Header("Close")] 
        [SerializeField] private UiAnimationType _closeType;
        [SerializeField] private float _closeStartValue;
        [SerializeField] private float _closeEndValue;
        [SerializeField] private float _closeDuration;
        [SerializeField] private Ease _closeEase;
        [SerializeField] private UiAnimationDirectionType _closeDirectionType;
        [SerializeField] private string _closeTriggerName;
        
        [Header("Targets")]
        [SerializeField] private RectTransform _transformTarget;
        [SerializeField] private CanvasGroup _canvasGroupTarget;
        [SerializeField] private Animator _animator;

        private Action _callback;
        
        public void PlayOpenAnimation(Action endCallback = null)
        {
            PlayAnimation(_openType, _openStartValue, _openEndValue, _openDuration, _openEase, _openDirectionType, _openTriggerName, endCallback);
        }
        
        public void PlayCloseAnimation(Action endCallback = null)
        {
            PlayAnimation(_closeType, _closeStartValue, _closeEndValue, _closeDuration, _closeEase, _closeDirectionType, _closeTriggerName, endCallback);
        }

        //For animation
        public void InvokeAnimationEvent()
        {
            _callback?.Invoke();
        }

        private void PlayAnimation(UiAnimationType type, float startValue, float endValue, float duration, Ease ease, 
            UiAnimationDirectionType directionType, string triggerName, Action callback)
        {
            switch (type)
            {
                case UiAnimationType.Scale:
                    PlayScale(startValue, endValue, duration, ease, callback);
                    break;
                case UiAnimationType.Move:
                    PlayMove(startValue, endValue, duration, ease, directionType, callback);
                    break;
                case UiAnimationType.Fade:
                    PlayFade(startValue, endValue, duration, ease, callback);
                    break;
                case UiAnimationType.Custom:
                    PlayCustom(triggerName, callback);
                    break;
            }
        }

        private void PlayScale(float startValue, float endValue, float duration, Ease ease, Action callback)
        {
            DOTween.Sequence()
                .Append(_transformTarget.DOScale(endValue, duration).From(startValue).SetEase(ease))
                .AppendInterval(duration)
                .AppendCallback(()=>
                {
                    callback?.Invoke();
                });
        }

        private void PlayMove(float startValue, float endValue, float duration, Ease ease, 
            UiAnimationDirectionType directionType, Action callback)
        {
            var tweener = directionType switch
            {
                UiAnimationDirectionType.X => _transformTarget.DOAnchorPosX(endValue, duration).From(new Vector2(startValue, _transformTarget.rect.y)).SetEase(ease),
                UiAnimationDirectionType.Y => _transformTarget.DOAnchorPosY(endValue, duration).From(new Vector2(_transformTarget.rect.x, startValue)).SetEase(ease),
                _ => null
            };

            if (tweener != null)
            {
                DOTween.Sequence()
                    .Append(tweener)
                    .AppendInterval(duration)
                    .AppendCallback(() =>
                    {
                        callback?.Invoke();
                    });
            }
            else
            {
                callback?.Invoke();
            }
        }
        
        private void PlayFade(float startValue, float endValue, float duration, Ease ease, Action callback)
        {
            DOTween.Sequence()
                .Append(_canvasGroupTarget.DOFade(endValue, duration).From(startValue).SetEase(ease))
                .AppendInterval(duration)
                .AppendCallback(()=>
                {
                    callback?.Invoke();
                });
        }
        
        private void PlayCustom(string triggerName, Action callback)
        {
            _animator.SetTrigger(triggerName);
            _callback = callback;
        }

        //for inspector
        private bool IsNoCustomOpenAnimationSelected()
        {
            return _openType != UiAnimationType.Custom;
        }
        //for inspector
        private bool IsNoCustomCloseAnimationSelected()
        {
            return _closeType != UiAnimationType.Custom;
        }
        //for inspector
        private bool IsMoveOpenAnimationSelected()
        {
            return _openType == UiAnimationType.Move;
        }
        //for inspector
        private bool IsMoveCloseAnimationSelected()
        {
            return _closeType == UiAnimationType.Move;
        }
        //for inspector
        private bool IsCustomAnimationSelected()
        {
            return _openType == UiAnimationType.Custom || _closeType == UiAnimationType.Custom;
        }
        //for inspector
        private bool IsCustomOpenAnimationSelected()
        {
            return _openType == UiAnimationType.Custom;
        }
        //for inspector
        private bool IsCustomCloseAnimationSelected()
        {
            return _closeType == UiAnimationType.Custom;
        }
    }
}