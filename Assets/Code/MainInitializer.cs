using Code.Modules.ControlModule;
using Code.Modules.SpaceshipModule;
using Code.Modules.StarSystemsModule;
using Code.Modules.UiModules.MainMenuModule;
using Code.Modules.UiModules.SelectedSpaceshipWindowModule;
using Plugins.Injection;

namespace Code
{
    public static class MainInitializer
    {
        public static void Initialize(EasyDi container)
        {
            StarSystemsModule.AddService(container);
            SpaceshipModule.AddService(container);
            
            ControlModule.Add(container);
            SpaceshipModule.Add(container);
            
            //Ui
            MainMenuWindowModule.Add(container);
            SelectedSpaceshipWindowModule.Add(container);
        }
    }
}