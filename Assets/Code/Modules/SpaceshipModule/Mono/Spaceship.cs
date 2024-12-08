using System;
using Cinemachine;
using Code.Modules.ControlModule.Interfaces;
using Code.Modules.SpaceshipModule.Enums;
using UnityEngine;

namespace Code.Modules.SpaceshipModule.Mono
{
    public class Spaceship : MonoBehaviour
    {
        [SerializeField] public Rigidbody Rigidbody;
        [SerializeField] public Animator Animator;
        [SerializeField] public Transform CameraTarget;
        [SerializeField] public Transform CenterView;

        [Header("Movement")] 
        [SerializeField] public float YawTorque;
        [SerializeField] public float PitchTorque;
        [SerializeField] public float RollTorque;
        [SerializeField] public float Thrust;
        [SerializeField] public float UpThrust;
        [SerializeField] public float StrafeThrust;
        [SerializeField] [Range(0.001f, 0.999f)] public float ThrustGlideReduction;
        [SerializeField] [Range(0.001f, 0.999f)] public float UpDownGlideReduction;
        [SerializeField] [Range(0.001f, 0.999f)] public float LeftRightGlideReduction;

        [Header("External cells")] 
        [SerializeField] public SpaceShipExternalCellsGroup[] SpaceShipExternalCellsGroups;
        
        [HideInInspector] public float Glide;
        [HideInInspector] public float VerticalGlide;
        [HideInInspector] public float HorizontalGlide;
        [HideInInspector] public Quaternion CenterViewOriginalRotation;
        [HideInInspector] public bool IsUpperLanding;

        public IShipControl ShipControl;
        public IShipCameraControl CameraControl;

        public void BindControl(IShipControl shipControl)
        {
            ShipControl = shipControl;
        }

        public void UnBindControl()
        {
            ShipControl = null;
            CameraControl = null;
        }

        public void BindCamera(IShipCameraControl cameraControl, CinemachineVirtualCamera camera)
        {
            CameraControl = cameraControl;
            camera.Follow = CameraTarget;
            CenterViewOriginalRotation = CenterView.localRotation;
        }
    }

    [Serializable]
    public class SpaceShipExternalCellsGroup
    {
        public Transform[] PointCells;
        public SpaceshipWeaponCellType Type;
    }
}