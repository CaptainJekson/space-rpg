using Code.Modules.CameraModule.Mono;
using Code.Modules.ControlModule.Mono;
using Code.Modules.SpaceshipModule.Enums;
using Code.Modules.SpaceshipModule.Factories;
using Plugins.Injection;
using Plugins.Injection.Interfaces;
using UnityEngine;

namespace Code.Modules.ControlModule.Processors
{
    public class TestCreatePlayerShip : IUpdate
    {
        [Inject] private PlayerControl _playerControl;
        [Inject] private SpaceshipFactoryAndStorage _spaceshipFactory;
        [Inject] private VirtualCameraRoot _virtualCameraRoot;

        public void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                var spaceshipInstance = _spaceshipFactory.Create(SpaceshipModel.ExodusRider, Vector3.zero, new Vector3(0, 15, 0));
                spaceshipInstance.BindControl(_playerControl);
                _virtualCameraRoot.SpaceShipCamera.Follow = spaceshipInstance.CameraTarget;
            }
        }
    }
}