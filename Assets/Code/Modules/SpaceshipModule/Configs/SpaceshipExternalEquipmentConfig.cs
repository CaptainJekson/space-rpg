using System;
using Code.Modules.SpaceshipModule.Mono;
using UnityEngine;

namespace Code.Modules.SpaceshipModule.Configs
{
    [CreateAssetMenu(menuName = "Spaceships/ExternalEquipmentConfig", fileName = "ExternalEquipmentConfig")]
    public class SpaceshipExternalEquipmentConfig : ScriptableObject
    {
        [SerializeField] private SpaceShipExternalEquipmentData[] _externalEquipments;
        
        public SpaceShipExternalEquipmentData[] SpaceShips => _externalEquipments;

        public SpaceshipExternalEquipment GetExternalEquipmentByModel(string modelId)
        {
            foreach (var spaceShipData in _externalEquipments)
            {
                if (spaceShipData.ModelId != modelId)
                {
                    continue;
                }
                
                return spaceShipData.SpaceshipWeaponPrefab;
            }

            return null;
        }
    }
    
    [Serializable]
    public class SpaceShipExternalEquipmentData
    {
        public string ModelId;
        public Sprite Icon;
        public string Name;
        public string Description;
        public SpaceshipExternalEquipment SpaceshipWeaponPrefab;
    }
}