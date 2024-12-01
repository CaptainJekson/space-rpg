using Code.Modules.ControlModule;
using Code.Modules.SpaceshipModule;
using Code.Modules.UiModules.MainMenuModule;
using Code.Modules.UiModules.SelectedSpaceshipWindowModule;
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
            
            //Ui
            MainMenuWindowModule.Add(container);
            SelectedSpaceshipWindowModule.Add(container);
        }
    }
}