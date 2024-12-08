using Code.Modules.SpaceshipModule.Processors;
using Plugins.Injection;

namespace Code.Modules.SpaceshipModule
{
    public static class SpaceshipModule
    {
        public static void Add(EasyDi container)
        {
            container.NewAndRegister<SpaceshipFactoryAndStorage>();
            container.NewAndRegister<SpaceshipEquipmentInstallerAndStorage>();
            container.New<SpaceshipMovementController>();
            container.New<SpaceshipCameraRotateController>();
            container.New<SpaceshipLandingController>();
        }
    }
}