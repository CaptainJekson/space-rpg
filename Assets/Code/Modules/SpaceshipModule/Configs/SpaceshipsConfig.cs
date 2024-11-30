using System;
using System.Collections.Generic;
using Code.Modules.SpaceshipModule.Enums;
using Code.Modules.SpaceshipModule.Mono;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Modules.SpaceshipModule.Configs
{
    [CreateAssetMenu(menuName = "Spaceships/SpaceShipsConfig", fileName = "SpaceShipsConfig")]
    public class SpaceshipsConfig : ScriptableObject
    {
        [SerializeField] private List<SpaceShipData> _spaceShips;

        public Spaceship GetSpaceShipByModel(SpaceshipModel model)
        {
            foreach (var spaceShipData in _spaceShips)
            {
                if (spaceShipData.Model != model)
                {
                    continue;
                }
                
                return spaceShipData.SpaceshipPrefab;
            }

            return null;
        }
    }

    [Serializable]
    public class SpaceShipData
    {
        public SpaceshipModel Model;
        public Sprite Icon;
        public string Name;
        public string Description;
        public Spaceship SpaceshipPrefab;
    }
}