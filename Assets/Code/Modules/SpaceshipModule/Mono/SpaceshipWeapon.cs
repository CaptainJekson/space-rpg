using Code.Modules.SpaceshipModule.Enums;
using UnityEngine;

namespace Code.Modules.SpaceshipModule.Mono
{
    public class SpaceshipWeapon : SpaceshipExternalEquipment
    {
        [SerializeField] public Transform Transform;

        [SerializeField] public SpaceshipWeaponProjectile SpaceshipWeaponProjectile;
        [SerializeField] public float EnergyDamage;
        [SerializeField] public float KineticDamage;
        [SerializeField] public float ShootingRate;
        [SerializeField] public float ProjectileSpeed;
        [SerializeField] public int MagazineAmmo;
        [SerializeField] public int MaxAmmo;
    }
}