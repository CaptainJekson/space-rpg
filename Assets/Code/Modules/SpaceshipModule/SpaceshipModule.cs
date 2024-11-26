using Code.Modules.SpaceshipModule.Factories;
using Code.Modules.SpaceshipModule.Old;
using Plugins.Injection;
using SpaceshipController = Code.Modules.SpaceshipModule.Processors.SpaceshipController;

namespace Code.Modules.SpaceshipModule
{
    public static class SpaceshipModule
    {
        public static void AddStorages(EasyDi container)
        {
            container.NewAndRegister<SpaceshipFactoryAndStorage>();
        }
        
        public static void Add(EasyDi container)
        {
            container.NewAndRegister<SpaceshipController>();
            //container.NewAndRegister<TestUpdate>();
        }
    }
}