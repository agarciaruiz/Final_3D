using UnityEngine;

public class AmmoSupply : MonoBehaviour, ISupply
{
    private GameObject player;
    private Shooting shooting;
    private EquipmentManager manager;
    private PlayerController playerController;
    private AudioSource audioSource;
    private Inventory inventory;
    private Weapon currentWeapon;

    private int ammoToAdd;
    private int storageToAdd;

    private void Start()
    {
        GetReferences();
    }

    public void PickupSupply()
    {
        currentWeapon = inventory.GetItem(manager.currentlyEquipedWeapon);
        WeaponClass weaponScript = currentWeapon.prefab.GetComponent<WeaponClass>();

        if (currentWeapon.weaponStyle == WeaponStyle.Primary)
        {
            ammoToAdd = currentWeapon.magazineSize - weaponScript.currentAmmo;
            storageToAdd = currentWeapon.storedAmmo - weaponScript.currentAmmoStorage;
        }

        if (currentWeapon.weaponStyle == WeaponStyle.Secondary)
        {
            ammoToAdd = currentWeapon.magazineSize - weaponScript.currentAmmo;
            storageToAdd = currentWeapon.storedAmmo - weaponScript.currentAmmoStorage;
        }

        if (weaponScript.currentAmmo == currentWeapon.magazineSize)
        {
            Debug.Log("Magazine full");
        }
        else
        {
            audioSource.clip = playerController.pickupClip;
            audioSource.Play();
            weaponScript.AddAmmo(ammoToAdd, storageToAdd);
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PickupSupply();
        }
    }

    private void GetReferences()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        shooting = player.GetComponent<Shooting>();
        manager = player.GetComponent<EquipmentManager>();
        inventory = player.GetComponent<Inventory>();
        playerController = player.GetComponent<PlayerController>();
        audioSource = player.GetComponent<AudioSource>();
    }
}
