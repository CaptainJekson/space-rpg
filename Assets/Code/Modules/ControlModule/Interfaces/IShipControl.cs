using UnityEngine;

namespace Code.Modules.ControlModule.Interfaces
{
    public interface IShipControl
    {
        public float Thrust1D { get; set; }
        public float Strafe1D { get; set; }
        public float UpDown1D { get; set; }
        public float Roll1D { get; set; }
        public Vector2 PitchYaw { get; set; }
    }
}