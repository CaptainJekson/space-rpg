using Code.Modules.UiModules.SelectedSpaceshipWindowModule.Processors;
using Plugins.Injection;

namespace Code.Modules.UiModules.SelectedSpaceshipWindowModule
{
    public static class SelectedSpaceshipWindowModule
    {
        public static void Add(EasyDi container)
        {
            container.New<SelectedSpaceshipWindowButtonController>();
            container.New<SelectedSpaceshipWindowItemsController>();
        }
    }
}