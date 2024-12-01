using Code.Modules.UiModules.MainMenuModule.Processors;
using Plugins.Injection;

namespace Code.Modules.UiModules.MainMenuModule
{
    public static class MainMenuWindowModule
    {
        public static void Add(EasyDi container)
        {
            container.NewAndRegister<MainMenuWindowController>();
        }
    }
}