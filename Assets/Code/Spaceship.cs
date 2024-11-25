using UnityEngine;
using UnityEngine.InputSystem;

namespace Code
{
    public class Spaceship : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;

        [Header("Movement")] 
        [SerializeField] private float _yawTorque = 500f;
        [SerializeField] private float _pitchTorque = 1000f;
        [SerializeField] private float _rollTorque = 1000f;
        [SerializeField] private float _thrust = 100f;
        [SerializeField] private float _upThrust = 50f;
        [SerializeField] private float _strafeThrust = 50f;
        [SerializeField] [Range(0.001f, 0.999f)] private float _thrustGlideReduction = 0.999f;
        [SerializeField] [Range(0.001f, 0.999f)] private float _upDownGlideReduction = 0.111f;
        [SerializeField] [Range(0.001f, 0.999f)] private float _leftRightGlideReduction = 0.111f;

        private float _glide;
        private float _verticalGlide;
        private float _horizontalGlide;
        
        //inputs
        private float _thrust1D;
        private float _strafe1D;
        private float _upDown1D;
        private float _roll1D;
        private Vector2 _pitchYaw;

        private void FixedUpdate()
        {
            HandleMovement();
        }

        private void HandleMovement()
        {
            //roll
            _rigidbody.AddRelativeTorque(Vector3.back * _roll1D * _rollTorque * Time.deltaTime);
            //pitch
            _rigidbody.AddRelativeTorque(Vector3.right * Mathf.Clamp(-_pitchYaw.y, -1f, 1f) * _pitchTorque * Time.deltaTime);
            //yaw
            _rigidbody.AddRelativeTorque(Vector3.up * Mathf.Clamp(_pitchYaw.x, -1f, 1f) * _yawTorque * Time.deltaTime);
            
            //trust
            if (_thrust1D > 0.1f || _thrust1D < -0.1f)
            {
                _rigidbody.AddRelativeForce(Vector3.forward * _thrust1D * _thrust * Time.deltaTime);
                _glide = _thrust;
            }
            else 
            {
                _rigidbody.AddRelativeForce(Vector3.forward * _glide * Time.deltaTime);
                _glide *= _thrustGlideReduction;
            }
            
            //up down
            if (_upDown1D > 0.1f || _upDown1D < -0.1f)
            {
                _rigidbody.AddRelativeForce(Vector3.up * _upDown1D * _upThrust * Time.deltaTime);
                _verticalGlide = _upDown1D * _upThrust;
            }
            else
            {
                _rigidbody.AddRelativeForce(Vector3.up * _verticalGlide * Time.deltaTime);
                _verticalGlide *= _upDownGlideReduction;
            }
            
            //strafing
            if (_strafe1D > 0.1f || _strafe1D < -0.1f)
            {
                _rigidbody.AddRelativeForce(Vector3.right * _strafe1D * _upThrust * Time.deltaTime);
                _horizontalGlide *= _strafe1D * _strafeThrust;
            }
            else
            {
                _rigidbody.AddRelativeForce(Vector3.right * _horizontalGlide * Time.deltaTime);
                _horizontalGlide *= _leftRightGlideReduction;
            }
            
        }

        #region InputMethods

        public void OnThrust(InputAction.CallbackContext context)
        {
            _thrust1D = context.ReadValue<float>();
        }

        public void OnStrafe(InputAction.CallbackContext context)
        {
            _strafe1D = context.ReadValue<float>();
        }

        public void OnUpDown(InputAction.CallbackContext context)
        {
            _upDown1D = context.ReadValue<float>();
        }

        public void OnRoll(InputAction.CallbackContext context)
        {
            _roll1D = context.ReadValue<float>();
        }

        public void OnPitchYaw(InputAction.CallbackContext context)
        {
            _pitchYaw = context.ReadValue<Vector2>();
        }
        
        #endregion
    }
}