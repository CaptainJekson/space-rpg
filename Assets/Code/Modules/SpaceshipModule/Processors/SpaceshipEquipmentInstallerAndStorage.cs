using System.Collections.Generic;
using Code.Modules.SpaceshipModule.Configs;
using Code.Modules.SpaceshipModule.Mono;
using Plugins.Injection;
using Plugins.Injection.Interfaces;
using UnityEngine;

namespace Code.Modules.SpaceshipModule.Processors
{
    public class SpaceshipEquipmentInstallerAndStorage : IAwake
    {
        [Inject] private SpaceshipFactoryAndStorage _spaceshipFactoryAndStorage;
        [Inject] private SpaceshipExternalEquipmentConfig _externalEquipmentConfig;

        private Dictionary<Spaceship, List<SpaceshipExternalEquipment>> _externalEquipmentBySpaceship;

        public void OnAwake()
        {
            _externalEquipmentBySpaceship = new Dictionary<Spaceship, List<SpaceshipExternalEquipment>>();
            _spaceshipFactoryAndStorage.Destroyed += OnSpaceshipDestroyed;
        }

        public void InstallExternalToPlayerShip(string modelId, int cellsGroupIndex)
        {
            if (!_spaceshipFactoryAndStorage.TryGetShipByPlayerControlled(out var spaceship))
            {
                Debug.LogError("[SpaceshipEquipmentInstaller.InstallToPlayerShip] " +
                               "The player does not control any ship");
                return;
            }

            if (cellsGroupIndex >= spaceship.SpaceShipExternalCellsGroups.Length)
            {
                Debug.LogError($"[SpaceshipEquipmentInstaller.InstallToPlayerShip] " +
                               $"there is no group of cells with this index, cellsGroupIndex = {cellsGroupIndex}");
                return;
            }

            var spaceshipExternalEquipment = _externalEquipmentConfig.GetExternalEquipmentByModel(modelId);
            var targetCellsGroup = spaceship.SpaceShipExternalCellsGroups[cellsGroupIndex];

            if (spaceshipExternalEquipment.CellType != targetCellsGroup.Type)
            {
                Debug.LogError($"[SpaceshipEquipmentInstaller.InstallToPlayerShip] " +
                               $"attempt to install into a cell of the wrong type, " +
                               $"equipment type {spaceshipExternalEquipment.CellType}, target type = {targetCellsGroup.Type}");
                return;
            }

            foreach (var cellTransform in targetCellsGroup.PointCells)
            {
                var equipmentInstance = Object.Instantiate(spaceshipExternalEquipment, cellTransform);
                AddOrUpdate(spaceship, equipmentInstance);
            }
        }

        private void AddOrUpdate(Spaceship spaceship, SpaceshipExternalEquipment equipmentInstance)
        {
            if (_externalEquipmentBySpaceship.TryGetValue(spaceship, out var equipments))
            {
                equipments.Add(equipmentInstance);
            }
            else
            {
                _externalEquipmentBySpaceship.Add(spaceship, new List<SpaceshipExternalEquipment>
                {
                    equipmentInstance,
                });
            }
        }
        
        private void OnSpaceshipDestroyed(Spaceship spaceship)
        {
            _externalEquipmentBySpaceship.Remove(spaceship);
        }
    }
}