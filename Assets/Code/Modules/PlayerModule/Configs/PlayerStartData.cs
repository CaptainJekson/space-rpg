using Code.Modules.StarSystemsModule.Enums;
using UnityEngine;

namespace Code.Modules.PlayerModule.Configs
{
    [CreateAssetMenu(menuName = "Player/PlayerStartData", fileName = "PlayerStartData")]
    public class PlayerStartData : ScriptableObject
    {
        [SerializeField] private StarSystemType _startStarSystem;
        [SerializeField] private Vector3 _startShipPosition;
        [SerializeField] private Vector3 _startShipRotation;
        
        public StarSystemType StartStarSystem => _startStarSystem;
        public Vector3 StartShipPosition => _startShipPosition;
        public Vector3 StartShipRotation => _startShipRotation;
    }
}