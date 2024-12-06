using Code.Modules.SpaceshipModule.Enums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Modules.UiModules.SelectedSpaceshipWindowModule.Mono
{
    public class SpaceShipUiItem : MonoBehaviour
    {
        [SerializeField] public Toggle Toggle;
        [SerializeField] public TextMeshProUGUI NameText;
        [SerializeField] public Image Icon;

        [HideInInspector] public string SpaceshipModelId;
    }
}