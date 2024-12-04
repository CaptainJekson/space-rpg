using Code.Modules.UiBaseModule.Configs;
using Code.Modules.UiBaseModule.Enums;
using Code.Modules.UiBaseModule.Mono;
using Code.Modules.UiModules.MainMenuWindowModule.Mono;
using Code.Modules.UiModules.SelectedSpaceshipWindowModule.Mono;
using Plugins.Injection;
using Plugins.Injection.Interfaces;

namespace Code.Modules.UiModules.MainMenuWindowModule.Processors
{
    public class MainMenuWindowController : IAwake
    {
        [Inject] private UiRoot _uiRoot;
        [Inject] private UiWindowsConfig _uiWindowsConfig;
        [Inject] private MainMenuWindow _mainMenuWindow;
        
        public void OnAwake()
        {
            _uiRoot.OnInitialize<MainMenuWindow>(Initialize);
            _uiRoot.RegisterUiElement<MainMenuWindow>(_mainMenuWindow);
        }

        private void Initialize(MainMenuWindow mainMenuWindow)
        {
            mainMenuWindow.StartGameButton.onClick.AddListener(OnStartGameButtonClick);
        }

        private void OnStartGameButtonClick()
        {
            _uiRoot.OpenOrCreate<SelectedSpaceshipWindow>(UiCanvasType.Window, _uiWindowsConfig.SpaceshipSelect);
        }
    }
}