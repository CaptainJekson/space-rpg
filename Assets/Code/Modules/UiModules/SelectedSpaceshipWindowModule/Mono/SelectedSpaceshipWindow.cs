using Code.Modules.SpaceshipModule.Enums;
using Code.Modules.UiBaseModule.Mono;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Modules.UiModules.SelectedSpaceshipWindowModule.Mono
{
    public class SelectedSpaceshipWindow : UiElement
    {
        [SerializeField] public Button CloseButton;
        [SerializeField] public Button StartGameButton;
        [SerializeField] public Button CloseOverlayButton;
        [SerializeField] public SpaceShipUiItem SpaceShipUiItem;
        [SerializeField] public Transform SpaceShipUiItemParent;
        [SerializeField] public ToggleGroup ToggleGroup;

        [HideInInspector] public SpaceshipModel SelectedSpaceShip;
    }
}