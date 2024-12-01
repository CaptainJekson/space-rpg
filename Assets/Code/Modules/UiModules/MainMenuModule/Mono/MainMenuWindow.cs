using Code.Modules.UiBaseModule.Mono;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Modules.UiModules.MainMenuModule.Mono
{
    public class MainMenuWindow : UiElement
    {
        [SerializeField] public Button StartGameButton;
        [SerializeField] public Button SettingButton;
        [SerializeField] public Button ExitButton;
    }
}