using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Items/Weapon")]
public class Weapon : Item
{
    public GameObject prefab;
    public ParticleSystem muzzleFlashParticles;
    public ParticleSystem hitParticles;
    public AudioClip fireFX;
    public AudioClip reloadFX;
    public int magazineSize;
    public int storedAmmo;
    public int damage;
    public float fireRate;
    public float range;
    public WeaponType weaponType;
    public WeaponStyle weaponStyle;
}

public enum WeaponType { Handgun, Machinegun}
public enum WeaponStyle { Primary, Secondary}
