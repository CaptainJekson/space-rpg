using Code.Modules.UiModules.MainMenuWindowModule.Processors;
using Plugins.Injection;

namespace Code.Modules.UiModules.MainMenuWindowModule
{
    public static class MainMenuWindowModule
    {
        public static void Add(EasyDi container)
        {
            container.NewAndRegister<MainMenuWindowController>();
        }
    }
}