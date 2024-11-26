using Code.Modules.ControlModule.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Modules.ControlModule.Mono
{
    public class PlayerControl : MonoBehaviour, IShipControl
    {
        public float Thrust1D { get; set; }
        public float Strafe1D { get; set; }
        public float UpDown1D { get; set; }
        public float Roll1D { get; set; }
        public Vector2 PitchYaw { get; set; }
        
        public void OnThrust(InputAction.CallbackContext context)
        {
            Thrust1D = context.ReadValue<float>();
        }

        public void OnStrafe(InputAction.CallbackContext context)
        {
            Strafe1D = context.ReadValue<float>();
        }

        public void OnUpDown(InputAction.CallbackContext context)
        {
            UpDown1D = context.ReadValue<float>();
        }

        public void OnRoll(InputAction.CallbackContext context)
        {
            Roll1D = context.ReadValue<float>();
        }

        public void OnPitchYaw(InputAction.CallbackContext context)
        {
            PitchYaw = context.ReadValue<Vector2>();
        }
    }
}