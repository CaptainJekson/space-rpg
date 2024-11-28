using Code.Modules.ControlModule.Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Modules.SpaceshipModule.Mono
{
    public class Spaceship : MonoBehaviour
    {
        [SerializeField] public Rigidbody Rigidbody;
        [SerializeField] public Transform CameraTarget;

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
        
        [HideInInspector] public float Glide;
        [HideInInspector] public float VerticalGlide;
        [HideInInspector] public float HorizontalGlide;

        public IShipControl ShipControl;
        
        public void BindControl(IShipControl shipControl)
        {
            ShipControl = shipControl;
        }
    }
}