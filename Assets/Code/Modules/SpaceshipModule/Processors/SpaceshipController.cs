using Code.Modules.ControlModule.Interfaces;
using Code.Modules.SpaceshipModule.Factories;
using Code.Modules.SpaceshipModule.Mono;
using Plugins.Injection;
using Plugins.Injection.Interfaces;
using UnityEngine;

namespace Code.Modules.SpaceshipModule.Processors
{
    public class SpaceshipController : IFixedUpdate
    {
        [Inject] private SpaceshipFactoryAndStorage _spaceshipFactory;
        
        public void OnFixedUpdate()
        {
            foreach (var spaceship in _spaceshipFactory.SpaceshipsByGuid.Values)
            {
                if (spaceship.ShipControl == null)
                {
                    continue;
                }
                
                Roll(spaceship.ShipControl, spaceship);
                Pitch(spaceship.ShipControl, spaceship);
                Yaw(spaceship.ShipControl, spaceship);
                Trust(spaceship.ShipControl, spaceship);
                UpDown(spaceship.ShipControl, spaceship);
                Strafing(spaceship.ShipControl, spaceship);
            }
        }
        
        private void Strafing(IShipControl shipControl, Spaceship spaceship)
        {
            if (shipControl.Strafe1D is > 0.1f or < -0.1f)
            {
                spaceship.Rigidbody.AddRelativeForce(Vector3.right * (shipControl.Strafe1D * spaceship.UpThrust * Time.deltaTime));
                spaceship.HorizontalGlide *= shipControl.Strafe1D * spaceship.StrafeThrust;
            }
            else
            {
                spaceship.Rigidbody.AddRelativeForce(Vector3.right * (spaceship.HorizontalGlide * Time.deltaTime));
                spaceship.HorizontalGlide *= spaceship.LeftRightGlideReduction;
            }
        }

        private void UpDown(IShipControl shipControl, Spaceship spaceship)
        {
            if (shipControl.UpDown1D is > 0.1f or < -0.1f)
            {
                spaceship.Rigidbody.AddRelativeForce(Vector3.up * (shipControl.UpDown1D * spaceship.UpThrust * Time.deltaTime));
                spaceship.VerticalGlide = shipControl.UpDown1D * spaceship.UpThrust;
            }
            else
            {
                spaceship.Rigidbody.AddRelativeForce(Vector3.up * (spaceship.VerticalGlide * Time.deltaTime));
                spaceship.VerticalGlide *= spaceship.UpDownGlideReduction;
            }
        }

        private void Trust(IShipControl shipControl, Spaceship spaceship)
        {
            if (shipControl.Thrust1D is > 0.1f or < -0.1f)
            {
                spaceship.Rigidbody.AddRelativeForce(Vector3.forward * (shipControl.Thrust1D * spaceship.Thrust * Time.deltaTime));
                spaceship.Glide = spaceship.Thrust;
            }
            else
            {
                spaceship.Rigidbody.AddRelativeForce(Vector3.forward * (spaceship.Glide * Time.deltaTime));
                spaceship.Glide *= spaceship.ThrustGlideReduction;
            }
        }

        private void Yaw(IShipControl shipControl, Spaceship spaceship)
        {
            spaceship.Rigidbody.AddRelativeTorque(Vector3.up * (Mathf.Clamp(shipControl.PitchYaw.x, -1f, 1f) 
                                                                * spaceship.YawTorque * Time.deltaTime));
        }

        private void Pitch(IShipControl shipControl, Spaceship spaceship)
        {
            spaceship.Rigidbody.AddRelativeTorque(Vector3.right * (Mathf.Clamp(-shipControl.PitchYaw.y, -1f, 1f) 
                                                                   * spaceship.PitchTorque * Time.deltaTime));
        }

        private void Roll(IShipControl shipControl, Spaceship spaceship)
        {
            spaceship.Rigidbody.AddRelativeTorque(Vector3.back * (shipControl.Roll1D * spaceship.RollTorque * Time.deltaTime));
        }
    }
}