using Code.Modules.ControlModule.Interfaces;
using Code.Modules.SpaceshipModule.Mono;
using Plugins.Injection;
using Plugins.Injection.Interfaces;
using UnityEngine;

namespace Code.Modules.SpaceshipModule.Processors
{
    public class SpaceshipLandingController : IUpdate
    {
        private const string LoverTriggerName = "Lover";
        private const string UpperTriggerName = "Upper";
        
        [Inject] private SpaceshipFactoryAndStorage _spaceshipFactory;
        
        public void OnUpdate()
        {
            foreach (var spaceship in _spaceshipFactory.SpaceshipsByGuid.Values)
            {
                if (spaceship.ShipControl == null)
                {
                    continue;
                }

                SetStateLanding(spaceship.ShipControl, spaceship);
            }
        }

        private void SetStateLanding(IShipControl shipControl, Spaceship spaceship)
        {
            if (!shipControl.LandingGear)
            {
                return;
            }
            
            var animator = spaceship.Animator;
            
            animator.SetTrigger(spaceship.IsUpperLanding ? LoverTriggerName : UpperTriggerName);
            spaceship.IsUpperLanding = !spaceship.IsUpperLanding;
            
            shipControl.LandingGear = false; //todo костыль????
        }
    }
}