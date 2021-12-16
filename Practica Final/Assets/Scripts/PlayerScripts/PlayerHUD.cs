using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private ProgressBar healthBar;
    [SerializeField] private ProgressBar shieldBar;
    [SerializeField] private InventoryUI inventoryUI;

    public void UpdateHealth(float currentHealth, float maxHealth)
    {
        healthBar.SetValues(currentHealth, maxHealth);
    }

    public void UpdateShield(float currentShield, float maxShield)
    {
        shieldBar.SetValues(currentShield, maxShield);
    }

    public void UpdateWeaponUI(Weapon newWeapon)
    {
        inventoryUI.UpdateInfo(newWeapon.icon, newWeapon.magazineSize, newWeapon.storedAmmo);
    }

    public void UpdateAmmoUI(int currentAmmo, int storedAmmo)
    {
        inventoryUI.UpdateAmmoUI(currentAmmo, storedAmmo);
    }
}
