using UnityEngine;

namespace Code.Modules.UiBaseModule.Mono
{
    public abstract class UiElement : MonoBehaviour
    {
        private UiAnimator _uiAnimator;

        public UiAnimator UiAnimator => _uiAnimator;

        private void Awake()
        {
            gameObject.TryGetComponent(out _uiAnimator);
        }
    }
}