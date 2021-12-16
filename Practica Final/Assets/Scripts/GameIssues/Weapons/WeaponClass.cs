using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponClass: MonoBehaviour
{
    public Weapon weapon;
    public AudioSource audioSource;
    [HideInInspector] public Transform weaponBarrel;
    [HideInInspector] public int currentAmmo;
    [HideInInspector] public int currentAmmoStorage;
    [HideInInspector] public bool canShoot;
    [HideInInspector] public bool canReload;

    private bool magazineIsEmpty;
    private GameObject player;
    private PlayerHUD playerHUD;

    private void Start()
    {
        InitWeapon();
    }

    public void InitAmmo(Weapon weapon)
    {
        currentAmmo = weapon.magazineSize;
        currentAmmoStorage = weapon.storedAmmo;
    }   

    void InitAudio(Weapon weapon)
    {
        audioSource.clip = weapon.fireFX;
    }

    public void UseAmmo(int currentAmmoUsed, int currentStoredAmmoUsed)
    {
        if (currentAmmo <= 0)
        {
            magazineIsEmpty = true;
            CheckCanShoot();
        }
        else
        {
            currentAmmo -= currentAmmoUsed;
            currentAmmoStorage -= currentStoredAmmoUsed;
            playerHUD.UpdateAmmoUI(currentAmmo, currentAmmoStorage);
        }
    }

    public void AddAmmo(int currentAmmoAdded, int currentStoredAmmoAdded)
    {
        currentAmmo += currentAmmoAdded;
        currentAmmoStorage += currentStoredAmmoAdded;
        playerHUD.UpdateAmmoUI(currentAmmo, currentAmmoStorage);
    }

    public void Reload(int ammoToReload)
    {
        if (canReload)
        {
            if (currentAmmoStorage >= ammoToReload)
            {
                if (currentAmmo == weapon.magazineSize)
                {
                    canReload = false;
                    Debug.Log("Magazine full");
                }
                else
                    canReload = true;

                AddAmmo(ammoToReload, 0);
                UseAmmo(0, ammoToReload);

                magazineIsEmpty = false;
                CheckCanShoot();
            }
            else
                Debug.Log("Not enough ammo to reaload");
        }
        else
        {
            Debug.Log("Can't reload now");
        }
    }

    public void CheckCanShoot()
    {
        if (magazineIsEmpty)
            canShoot = false;
        else
            canShoot = true;
    }

    public void InitWeapon()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHUD = player.GetComponent<PlayerHUD>();
        audioSource = GetComponent<AudioSource>();
        weaponBarrel = transform.GetChild(0);
        canShoot = true;
        canReload = true;
        magazineIsEmpty = false;
        InitAudio(weapon);
        InitAmmo(weapon);
    }
}
