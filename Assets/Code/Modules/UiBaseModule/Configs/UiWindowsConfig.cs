using Code.Modules.UiBaseModule.Mono;
using UnityEngine;

namespace Code.Modules.UiBaseModule.Configs
{
    [CreateAssetMenu(menuName = "UI/UiWindowsConfig", fileName = "UiWindowsConfig")]
    public class UiWindowsConfig : ScriptableObject
    {
        [SerializeField] public UiElement SpaceshipSelect;
    }
}