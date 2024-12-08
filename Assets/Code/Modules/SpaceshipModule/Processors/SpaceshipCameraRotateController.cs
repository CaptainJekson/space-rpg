using Code.Modules.ControlModule.Interfaces;
using Code.Modules.SpaceshipModule.Mono;
using Plugins.Injection;
using Plugins.Injection.Interfaces;
using UnityEngine;

namespace Code.Modules.SpaceshipModule.Processors
{
    public class SpaceshipCameraRotateController : ILateUpdate
    {
        [Inject] private SpaceshipFactoryAndStorage _spaceshipFactory;

        private float _currentYaw;
        private float _currentPitch;
        private readonly float _rotationSpeed = 100;
        
        public void OnLateUpdate() 
        {
            foreach (var spaceship in _spaceshipFactory.SpaceshipsByGuid.Values)
            {
                if (spaceship.CameraControl == null)
                {
                    continue;
                }

                var cameraControl = spaceship.CameraControl;
                
                CameraView(cameraControl, spaceship);
                CameraViewCancel(cameraControl, spaceship);
            }
        }

        private void CameraView(IShipCameraControl shipControl, Spaceship spaceship)
        {
            if (shipControl.CameraView < 1.0f)
            {
                return;
            }

            _currentYaw += shipControl.CameraRotate.x * _rotationSpeed * Time.deltaTime;
            _currentPitch -= shipControl.CameraRotate.y * _rotationSpeed * Time.deltaTime;
            
            spaceship.CenterView.localRotation = Quaternion.Euler(_currentPitch, _currentYaw, 0f);
        }
        
        private void CameraViewCancel(IShipCameraControl shipControl, Spaceship spaceship)
        {
            if (shipControl.CameraViewCancel < 1.0f)
            {
                return;
            }
            
            _currentYaw = 0;
            _currentPitch = 0;
            spaceship.CenterView.localRotation = spaceship.CenterViewOriginalRotation;
            
            shipControl.CameraViewCancel = 0; //todo костыль????
        }
    }
}