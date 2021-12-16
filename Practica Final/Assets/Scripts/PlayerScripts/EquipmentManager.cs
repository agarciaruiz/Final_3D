using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EquipmentManager : MonoBehaviour
{
    [HideInInspector] public int currentlyEquipedWeapon = 0;

    [SerializeField] private Weapon defaultWeapon = null;
    private Inventory inventory;
    private PlayerHUD playerHUD;
    private PlayerInput playerInput;
    private InputAction equipPrimary;
    private InputAction equipSecondary;

    private void Start()
    {
        GetReferences();
        InitVariables();
    }

    private void Update()
    {
        if (equipPrimary.triggered && currentlyEquipedWeapon != 0)
        {
            //UnequipWeapon();
            EquipWeapon(inventory.GetItem(0));
        }
        if (equipSecondary.triggered && currentlyEquipedWeapon != 1)
        {
            if (inventory.GetItem(1) != null)
            {
                //UnequipWeapon();
                EquipWeapon(inventory.GetItem(1));
            }
        }
    }

    private void EquipWeapon(Weapon weapon)
    {
        int currentAmmo = weapon.prefab.GetComponent<WeaponClass>().currentAmmo;
        int currentAmmoStorage = weapon.prefab.GetComponent<WeaponClass>().currentAmmoStorage;
        playerHUD.UpdateWeaponUI(weapon);
        
        if ((int)weapon.weaponStyle == 0)
        {
            playerHUD.UpdateAmmoUI(currentAmmo, currentAmmoStorage);
        }
        if((int)weapon.weaponStyle == 1)
        {
            playerHUD.UpdateAmmoUI(currentAmmo, currentAmmoStorage);
        }

        currentlyEquipedWeapon = (int)weapon.weaponStyle;
    }

    /*private void UnequipWeapon()
    {
        Destroy(currentWeapon);
    }*/

    private void InitVariables()
    {
        inventory.AddItem(defaultWeapon);
        EquipWeapon(inventory.GetItem(0));
        equipPrimary = playerInput.actions["EquipPrimary"];
        equipSecondary = playerInput.actions["EquipSecondary"];
    }

    private void GetReferences()
    {
        inventory = GetComponent<Inventory>();
        playerInput = GetComponent<PlayerInput>();
        playerHUD = GetComponent<PlayerHUD>();
    }
}
