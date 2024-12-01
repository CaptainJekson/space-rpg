using Code.Modules.UiBaseModule.Mono;
using Code.Modules.UiModules.SelectedSpaceshipWindowModule.Mono;
using Plugins.Injection;
using Plugins.Injection.Interfaces;
using UnityEngine;

namespace Code.Modules.UiModules.SelectedSpaceshipWindowModule.Processors
{
    public class SelectedSpaceshipWindowButtonController : IAwake
    {
        [Inject] private UiRoot _uiRoot;

        private SelectedSpaceshipWindow _selectedSpaceshipWindow;

        public void OnAwake()
        {
            _uiRoot.OnInitialize<SelectedSpaceshipWindow>(Initialize);
        }

        private void Initialize(SelectedSpaceshipWindow selectedSpaceshipWindow)
        {
            _selectedSpaceshipWindow = selectedSpaceshipWindow;
            
            _selectedSpaceshipWindow.CloseButton.onClick.AddListener(OnCloseButtonClick);
            _selectedSpaceshipWindow.CloseOverlayButton.onClick.AddListener(OnCloseButtonClick);
            _selectedSpaceshipWindow.StartGameButton.onClick.AddListener(OnStartGameButtonClick);
        }

        private void OnCloseButtonClick()
        {
            _uiRoot.Close<SelectedSpaceshipWindow>();
        }

        private void OnStartGameButtonClick()
        {
            Debug.LogError($"selected model: {_selectedSpaceshipWindow.SelectedSpaceShip}");
        }
    }
}