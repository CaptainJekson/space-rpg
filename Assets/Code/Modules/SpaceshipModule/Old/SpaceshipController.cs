using Code.Modules.ControlModule.Interfaces;
using UnityEngine;

namespace Code.Modules.SpaceshipModule.Old
{
    public class SpaceshipController : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;

        [Header("Movement")] 
        [SerializeField] private float _yawTorque;
        [SerializeField] private float _pitchTorque;
        [SerializeField] private float _rollTorque;
        [SerializeField] private float _thrust;
        [SerializeField] private float _upThrust;
        [SerializeField] private float _strafeThrust;
        [SerializeField] [Range(0.001f, 0.999f)] private float _thrustGlideReduction;
        [SerializeField] [Range(0.001f, 0.999f)] private float _upDownGlideReduction;
        [SerializeField] [Range(0.001f, 0.999f)] private float _leftRightGlideReduction;

        private float _glide;
        private float _verticalGlide;
        private float _horizontalGlide;

        private IShipControl _shipControl;

        public void BindControl(IShipControl shipControl)
        {
            _shipControl = shipControl;
        }

        private void FixedUpdate()
        {
            if (_shipControl == null)
            {
                return;
            }
            
            HandleMovement();
        }

        private void HandleMovement()
        {
            Roll();
            Pitch();
            Yaw();
            Trust();
            UpDown();
            Strafing();
        }

        private void Strafing()
        {
            if (_shipControl.Strafe1D is > 0.1f or < -0.1f)
            {
                _rigidbody.AddRelativeForce(Vector3.right * (_shipControl.Strafe1D * _upThrust * Time.deltaTime));
                _horizontalGlide *= _shipControl.Strafe1D * _strafeThrust;
            }
            else
            {
                _rigidbody.AddRelativeForce(Vector3.right * (_horizontalGlide * Time.deltaTime));
                _horizontalGlide *= _leftRightGlideReduction;
            }
        }

        private void UpDown()
        {
            if (_shipControl.UpDown1D is > 0.1f or < -0.1f)
            {
                _rigidbody.AddRelativeForce(Vector3.up * (_shipControl.UpDown1D * _upThrust * Time.deltaTime));
                _verticalGlide = _shipControl.UpDown1D * _upThrust;
            }
            else
            {
                _rigidbody.AddRelativeForce(Vector3.up * (_verticalGlide * Time.deltaTime));
                _verticalGlide *= _upDownGlideReduction;
            }
        }

        private void Trust()
        {
            if (_shipControl.Thrust1D is > 0.1f or < -0.1f)
            {
                _rigidbody.AddRelativeForce(Vector3.forward * (_shipControl.Thrust1D * _thrust * Time.deltaTime));
                _glide = _thrust;
            }
            else
            {
                _rigidbody.AddRelativeForce(Vector3.forward * (_glide * Time.deltaTime));
                _glide *= _thrustGlideReduction;
            }
        }

        private void Yaw()
        {
            _rigidbody.AddRelativeTorque(Vector3.up * (Mathf.Clamp(_shipControl.PitchYaw.x, -1f, 1f) 
                                                       * _yawTorque * Time.deltaTime));
        }

        private void Pitch()
        {
            _rigidbody.AddRelativeTorque(Vector3.right * (Mathf.Clamp(-_shipControl.PitchYaw.y, -1f, 1f) 
                                                          * _pitchTorque * Time.deltaTime));
        }

        private void Roll()
        {
            _rigidbody.AddRelativeTorque(Vector3.back * (_shipControl.Roll1D * _rollTorque * Time.deltaTime));
        }
    }
}