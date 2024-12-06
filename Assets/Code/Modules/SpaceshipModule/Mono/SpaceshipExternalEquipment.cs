using Code.Modules.SpaceshipModule.Enums;
using UnityEngine;

namespace Code.Modules.SpaceshipModule.Mono
{
    public abstract class SpaceshipExternalEquipment : MonoBehaviour
    {
        [SerializeField] public SpaceshipWeaponCellType CellType;
    }
}