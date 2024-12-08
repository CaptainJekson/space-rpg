using Code.Modules.CameraModule.Mono;
using Code.Modules.ControlModule.Mono;
using Code.Modules.PlayerModule.Configs;
using Code.Modules.SpaceshipModule.Enums;
using Code.Modules.SpaceshipModule.Mono;
using Code.Modules.SpaceshipModule.Processors;
using Code.Modules.StarSystemsModule.Configs;
using Code.Modules.StarSystemsModule.Processors;
using Code.Modules.UiBaseModule.Mono;
using Code.Modules.UiModules.MainMenuWindowModule.Mono;
using Code.Modules.UiModules.SelectedSpaceshipWindowModule.Mono;
using Plugins.Injection;
using Plugins.Injection.Interfaces;
using UnityEngine;

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
        [Inject] private SpaceshipEquipmentInstallerAndStorage _spaceshipEquipmentInstallerAndStorage;

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

        private void OnStartGameButtonClick() //todo test
        {
            var starSystemType = _playerStartData.StartStarSystem;
            
            _starSystemLoader.Load(starSystemType, () =>
            {
                var spaceShipModel = _selectedSpaceshipWindow.SelectedSpaceshipModelId;
                var spaceshipInstance = _spaceshipFactory.Create(spaceShipModel, _playerStartData.StartShipPosition,
                    _playerStartData.StartShipRotation);

                spaceshipInstance.BindControl(_playerControl);
                spaceshipInstance.BindCamera(_playerControl, _virtualCameraRoot.SpaceShipCamera);
                
                _spaceshipEquipmentInstallerAndStorage.InstallExternalToPlayerShip("RocketGradLevel5", 0);
                _spaceshipEquipmentInstallerAndStorage.InstallExternalToPlayerShip("PlasmaGunLevel5", 1);
                _spaceshipEquipmentInstallerAndStorage.InstallExternalToPlayerShip("TurretMinigunLevel5", 2);

                _spaceship1 = spaceshipInstance;
                _uiRoot.Close<MainMenuWindow>();
                _uiRoot.Close<SelectedSpaceshipWindow>();
                
                //two test ships VortexCorvette StellarDropship
                _spaceship2 = _spaceshipFactory.Create("VortexCorvette", new Vector3(52f, 51f, 65.6f),
                    Vector3.zero);
            });
        }

        private Spaceship _spaceship1;
        private Spaceship _spaceship2;

        public void OnUpdate() //todo for test: switch ship
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                _spaceship1.BindControl(_playerControl);
                _spaceship1.BindCamera(_playerControl, _virtualCameraRoot.SpaceShipCamera);
                _spaceship2.UnBindControl();
            }

            if (Input.GetKeyDown(KeyCode.U))
            {
                _spaceship2.BindControl(_playerControl);
                _spaceship2.BindCamera(_playerControl, _virtualCameraRoot.SpaceShipCamera);
                _spaceship1.UnBindControl();
            }
        }
    }
}