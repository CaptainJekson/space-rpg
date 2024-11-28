using Cinemachine;
using UnityEngine;

namespace Code.Modules.CameraModule.Mono
{
    public class VirtualCameraRoot : MonoBehaviour
    {
        [SerializeField] public CinemachineVirtualCamera SpaceShipCamera;
        [SerializeField] public CinemachineVirtualCamera HumanoidCamera;
    }
}