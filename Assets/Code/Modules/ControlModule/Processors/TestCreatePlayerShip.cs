using Code.Modules.ControlModule.Mono;
using Code.Modules.SpaceshipModule.Enums;
using Code.Modules.SpaceshipModule.Factories;
using Plugins.Injection;
using Plugins.Injection.Interfaces;
using UnityEngine;

namespace Code.Modules.ControlModule.Processors
{
    public class TestCreatePlayerShip : IAwake, IUpdate
    {
        [Inject] private PlayerControl _playerControl;
        [Inject] private SpaceshipFactoryAndStorage _spaceshipFactory;

        public void OnAwake()
        {
            var spaceshipInstance = _spaceshipFactory.Create(SpaceshipModel.CobraMk5, Vector3.zero, Vector3.zero);
            spaceshipInstance.BindControl(_playerControl);
        }

        public void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                var spaceshipInstance = _spaceshipFactory.Create(SpaceshipModel.CobraMk5, Vector3.zero, new Vector3(0, 15, 0));
                spaceshipInstance.BindControl(_playerControl);
            }
        }
    }
}