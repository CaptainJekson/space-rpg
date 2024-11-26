using Code.Modules.ControlModule.Processors;
using Plugins.Injection;

namespace Code.Modules.ControlModule
{
    public static class ControlModule
    {
        public static void Add(EasyDi container)
        {
            container.New<TestCreatePlayerShip>(); //todo это для теста следующим шагом заассайнить камеру, далее сделать подгрузку мира
        }
    }
}