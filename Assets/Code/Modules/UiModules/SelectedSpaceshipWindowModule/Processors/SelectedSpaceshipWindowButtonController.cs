using Code.Modules.CameraModule.Mono;
using Code.Modules.ControlModule.Mono;
using Code.Modules.PlayerModule.Configs;
using Code.Modules.SpaceshipModule.Factories;
using Code.Modules.SpaceshipModule.Mono;
using Code.Modules.StarSystemsModule.Configs;
using Code.Modules.StarSystemsModule.Processors;
using Code.Modules.UiBaseModule.Mono;
using Code.Modules.UiModules.MainMenuModule.Mono;
using Code.Modules.UiModules.SelectedSpaceshipWindowModule.Mono;
using Plugins.Injection;
using Plugins.Injection.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Modules.UiModules.SelectedSpaceshipWindowModule.Processors
{
    public class SelectedSpaceshipWindowButtonController : IAwake , IUpdate
    {
        [Inject] private UiRoot _uiRoot;
        [Inject] private PlayerStartData _playerStartData;
        [Inject] private StarSystemLoader _starSystemLoader;
        [Inject] private SpaceshipFactoryAndStorage _spaceshipFactory;
        [Inject] private StarSystemsConfig _starSystemsConfig;
        [Inject] private PlayerControl _playerControl;
        [Inject] private VirtualCameraRoot _virtualCameraRoot;

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
            var starSystemType = _playerStartData.StartStarSystem;
            
            _starSystemLoader.Load(starSystemType, () =>
            {
                var spaceShipModel = _selectedSpaceshipWindow.SelectedSpaceShip;
                var spaceshipInstance = _spaceshipFactory.Create(spaceShipModel, _playerStartData.StartShipPosition,
                    _playerStartData.StartShipRotation);
                
                //todo проверить точно ли это вообще нужно!!!!
                var starSystemData = _starSystemsConfig.GetStarSystemByType(starSystemType);
                var targetScene = SceneManager.GetSceneByName(starSystemData.Scene.name);
                
                SceneManager.MoveGameObjectToScene(spaceshipInstance.gameObject, targetScene);
                //todo end 
                
                spaceshipInstance.BindControl(_playerControl);
                spaceshipInstance.BindCamera(_virtualCameraRoot.SpaceShipCamera);

                _spaceship = spaceshipInstance;
                _uiRoot.Close<MainMenuWindow>();
                _uiRoot.Close<SelectedSpaceshipWindow>();
            });
        }

        private Spaceship _spaceship;

        public void OnUpdate() //todo for test
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                _spaceship.BindControl(_playerControl);
            }

            if (Input.GetKeyDown(KeyCode.U))
            {
                _spaceship.UnBindControl();
            }
        }
    }
}