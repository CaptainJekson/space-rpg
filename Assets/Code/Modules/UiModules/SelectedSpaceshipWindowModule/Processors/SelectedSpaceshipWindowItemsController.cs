using Code.Modules.SpaceshipModule.Configs;
using Code.Modules.UiBaseModule.Mono;
using Code.Modules.UiModules.SelectedSpaceshipWindowModule.Mono;
using Plugins.Injection;
using Plugins.Injection.Interfaces;
using UnityEngine;

namespace Code.Modules.UiModules.SelectedSpaceshipWindowModule.Processors
{
    public class SelectedSpaceshipWindowItemsController : IAwake
    {
        [Inject] private UiRoot _uiRoot;
        [Inject] private SpaceshipsConfig _spaceshipsConfig;
        
        private SelectedSpaceshipWindow _selectedSpaceshipWindow;
        
        public void OnAwake()
        {
            _uiRoot.OnInitialize<SelectedSpaceshipWindow>(Initialize);
        }

        private void Initialize(SelectedSpaceshipWindow selectedSpaceshipWindow)
        {
            _selectedSpaceshipWindow = selectedSpaceshipWindow;
            CreateItems();
        }
        
        private void CreateItems()
        {
            foreach (var spaceShipData in _spaceshipsConfig.SpaceShips)
            {
                var itemInstance = Object.Instantiate(_selectedSpaceshipWindow.SpaceShipUiItem,
                    _selectedSpaceshipWindow.SpaceShipUiItemParent);

                itemInstance.Icon.sprite = spaceShipData.Icon;
                itemInstance.NameText.text = spaceShipData.Name;
                itemInstance.Toggle.group = _selectedSpaceshipWindow.ToggleGroup;
                itemInstance.SpaceshipModel = spaceShipData.Model;
                
                itemInstance.Toggle.onValueChanged.AddListener(isOn =>
                {
                    if (!isOn)
                    {
                        return;
                    }

                    _selectedSpaceshipWindow.SelectedSpaceShip = spaceShipData.Model;
                });
            }
        }
    }
}