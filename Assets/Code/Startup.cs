using Code.Modules.ControlModule.Mono;
using Plugins.Injection;
using UnityEngine;

namespace Code
{
    public class Startup : MonoBehaviour
    {
        [SerializeField] private PlayerControl _playerControl;

        [SerializeField] private ScriptableObjectsEasyDi _configs;
        
        private EasyDi _container;
        
        private void Start()
        {
            _container = new EasyDi();
            _container.CopyRegistrationsFrom(_configs);
            
            // _container.Register(uiRoot);
            // _container.Register(mainCamera);
            _container.Register(_playerControl);
            
            MainInitializer.Initialize(_container);
        }
    }
}