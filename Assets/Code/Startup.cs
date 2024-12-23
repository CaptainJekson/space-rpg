using Code.Modules.CameraModule.Mono;
using Code.Modules.ControlModule.Mono;
using Code.Modules.UiBaseModule.Mono;
using Code.Modules.UiModules.MainMenuWindowModule.Mono;
using Plugins.Injection;
using UnityEngine;

namespace Code
{
    public class Startup : MonoBehaviour
    {
        [SerializeField] private PlayerControl _playerControl;
        [SerializeField] private VirtualCameraRoot _cameraRoot;
        [SerializeField] private UiRoot _uiRoot;
        [SerializeField] private MainMenuWindow _mainMenuWindow;

        [SerializeField] private ScriptableObjectsEasyDi _configs;
        
        private EasyDi _container;
        
        private void Start()
        {
            _container = new EasyDi();
            _container.CopyRegistrationsFrom(_configs);
            
            _container.Register(_cameraRoot);
            _container.Register(_uiRoot);
            _container.Register(_playerControl);
            _container.Register(_mainMenuWindow);
            
            MainInitializer.Initialize(_container);
        }
    }
}