using Code.Modules.ControlModule;
using Code.Modules.SpaceshipModule;
using Plugins.Injection;

namespace Code
{
    public static class MainInitializer
    {
        public static void Initialize(EasyDi container)
        {
            SpaceshipModule.AddStorages(container);
            
            ControlModule.Add(container);
            SpaceshipModule.Add(container);
        }
    }
}