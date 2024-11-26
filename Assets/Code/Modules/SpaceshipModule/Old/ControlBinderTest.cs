using Code.Modules.ControlModule.Mono;
using UnityEngine;

namespace Code.Modules.SpaceshipModule.Old
{
    public class ControlBinderTest : MonoBehaviour
    {
        [SerializeField] private PlayerControl _playerControl;
        [SerializeField] private SpaceshipController _spaceshipController;

        private void Awake()
        {
            _spaceshipController.BindControl(_playerControl);
        }
    }
}