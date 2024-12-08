using UnityEngine;

namespace Code.Modules.ControlModule.Interfaces
{
    public interface IShipCameraControl
    {
        public float CameraView { get; set; }
        public float CameraViewCancel { get; set; }
        public Vector2 CameraRotate { get; set; }
    }
}