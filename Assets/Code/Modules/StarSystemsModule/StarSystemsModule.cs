using Code.Modules.StarSystemsModule.Processors;
using Plugins.Injection;

namespace Code.Modules.StarSystemsModule
{
    public static class StarSystemsModule
    {
        public static void Add(EasyDi container)
        {
            container.NewAndRegister<StarSystemLoader>();
        }
    }
}