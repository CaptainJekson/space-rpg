using System;
using System.Collections.Generic;
using Code.Modules.SpaceshipModule.Configs;
using Code.Modules.SpaceshipModule.Enums;
using Code.Modules.SpaceshipModule.Mono;
using Plugins.Injection;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Modules.SpaceshipModule.Factories
{
    public class SpaceshipFactoryAndStorage
    {
        [Inject] private SpaceshipsConfig _spaceshipsConfig;
        
        private readonly Dictionary<Guid, Spaceship> _spaceshipsByGuid;

        public Dictionary<Guid, Spaceship> SpaceshipsByGuid => _spaceshipsByGuid;

        public SpaceshipFactoryAndStorage()
        {
            _spaceshipsByGuid = new Dictionary<Guid, Spaceship>();
        }

        public Spaceship Create(SpaceshipModel model, Vector3 position, Vector3 rotation)
        {
            var template = _spaceshipsConfig.GetSpaceShipByModel(model);
            var instance = Object.Instantiate(template, position, Quaternion.Euler(rotation));
            _spaceshipsByGuid.Add(Guid.NewGuid(), instance);

            return instance;
        }

        public void Destroy(string spaceshipGuid)
        {
            if (!_spaceshipsByGuid.TryGetValue(new Guid(spaceshipGuid), out var instance))
            {
                return;
            }
            
            Object.Destroy(instance.gameObject);
            _spaceshipsByGuid.Remove(new Guid(spaceshipGuid));
        }
    }
}