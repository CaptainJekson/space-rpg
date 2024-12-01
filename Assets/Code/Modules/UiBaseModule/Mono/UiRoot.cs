using System;
using System.Collections.Generic;
using Code.Modules.UiBaseModule.Enums;
using UnityEngine;
using Delegate = System.Delegate;

namespace Code.Modules.UiBaseModule.Mono
{
    public class UiRoot : MonoBehaviour
    {
        [SerializeField] private Transform _hud;
        [SerializeField] private Transform _windows;
        [SerializeField] private Transform _popups;

        private Dictionary<Type, UiElement> _uiElements;
        private Dictionary<Type, List<Delegate>> _handlers;

        private void Awake()
        {
            _uiElements = new Dictionary<Type, UiElement>();
            _handlers = new Dictionary<Type, List<Delegate>>();
        }

        public void RegisterUiElement<T>(UiElement uiElementInstance) where T : UiElement
        {
            _uiElements.Add(uiElementInstance.GetType(), uiElementInstance);
            InitializeInvoke<T>(uiElementInstance);
        }

        public void OpenOrCreate<T>(UiCanvasType canvasType, UiElement uiElementTemplate) where T : UiElement
        {
            if (_uiElements.TryGetValue(typeof(T), out var uiElement))
            {
                SetStateUiElement(uiElement, true);
            }
            else
            {
                CreateElement<T>(canvasType, uiElementTemplate);
            }
        }

        public void OnInitialize<T>(Action<T> handler) where T : UiElement
        {
            if (_handlers.TryGetValue(typeof(T), out var handlers))
            {
                handlers.Add(handler);
            }
            else
            {
                _handlers.Add(typeof(T), new List<Delegate> { handler });
            }
        }

        public void Close<T>() where T : UiElement
        {
            if (_uiElements.TryGetValue(typeof(T), out var uiElement))
            {
                SetStateUiElement(uiElement, false);
            }
        }

        private void CreateElement<T>(UiCanvasType canvasType, UiElement uiElementTemplate) where T : UiElement
        {
            var uiElementInstance = canvasType switch
            {
                UiCanvasType.Hud => Instantiate(uiElementTemplate, _hud),
                UiCanvasType.Window => Instantiate(uiElementTemplate, _windows),
                UiCanvasType.Popup => Instantiate(uiElementTemplate, _popups),
                _ => null
            };

            SetStateUiElement(uiElementInstance, true);

            if (uiElementInstance != null)
            {
                _uiElements.Add(uiElementInstance.GetType(), uiElementInstance);
            }
            
            InitializeInvoke<T>(uiElementInstance);
        }

        private void InitializeInvoke<T>(UiElement uiElementInstance) where T : UiElement
        {
            if (!_handlers.TryGetValue(typeof(T), out var handlers))
            {
                return;
            }
            
            foreach (var handler in handlers)
            {
                handler.DynamicInvoke(uiElementInstance);
            }
        }

        private void SetStateUiElement(UiElement uiElement, bool state)
        {
            if (uiElement.UiAnimator == null)
            {
                uiElement.gameObject.SetActive(state);
            }
            else
            {
                if (state)
                {
                    uiElement.gameObject.SetActive(true);
                    uiElement.UiAnimator.PlayOpenAnimation();
                }
                else
                {
                    uiElement.UiAnimator.PlayCloseAnimation(() => { uiElement.gameObject.SetActive(false); });
                }
            }
        }
    }
}