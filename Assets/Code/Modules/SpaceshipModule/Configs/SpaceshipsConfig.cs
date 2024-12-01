using System;
using Code.Modules.SpaceshipModule.Enums;
using Code.Modules.SpaceshipModule.Mono;
using UnityEngine;

namespace Code.Modules.SpaceshipModule.Configs
{
    [CreateAssetMenu(menuName = "Spaceships/SpaceShipsConfig", fileName = "SpaceShipsConfig")]
    public class SpaceshipsConfig : ScriptableObject
    {
        [SerializeField] private SpaceShipData[] _spaceShips;

        public SpaceShipData[] SpaceShips => _spaceShips;

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