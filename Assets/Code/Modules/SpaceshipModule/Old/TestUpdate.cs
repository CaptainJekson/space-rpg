using Plugins.Injection.Interfaces;
using UnityEngine;

namespace Code.Modules.SpaceshipModule.Old
{
    public class TestUpdate : IUpdate
    {
        public void OnUpdate()
        {
            Debug.LogError("se");
        }
    }
}