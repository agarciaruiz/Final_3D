using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations.Rigging;

public class Shooting : MonoBehaviour
{
    [SerializeField] private Rig aimLayer;
    [SerializeField] private float aimDuration = 0.1f;
    [SerializeField] private Transform target;
    [SerializeField] private LayerMask zombieLayer;

    private Inventory inventory;
    private EquipmentManager equipmentManager;
    private Animator animator;
    private AudioSource gunAudio;
    private float lastFireTime;

    [HideInInspector] public float soundIntensity = 100f;

    private void Awake()
    {
        GetReferences();
    }

    void Update()
    {
        if (Mouse.current.rightButton.isPressed)
        {
            StartAim();
        }

        if (Mouse.current.rightButton.wasReleasedThisFrame)
        {
            CancelAim();
        }

        if (Mouse.current.leftButton.isPressed)
        {
            Weapon currentWeapon = inventory.GetItem(equipmentManager.currentlyEquipedWeapon);
            Shoot(currentWeapon);
        }

        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            Weapon currentWeapon = inventory.GetItem(equipmentManager.currentlyEquipedWeapon);
            WeaponClass weaponClass = currentWeapon.prefab.GetComponent<WeaponClass>();

            int ammoToReload = currentWeapon.magazineSize - weaponClass.currentAmmo;
            weaponClass.Reload(ammoToReload);
            PlayAudio(weaponClass.weapon.reloadFX);
        }
    }

    private void RaycastShoot(Weapon currentWeapon)
    {
        Ray ray = new Ray();
        RaycastHit hit;

        float currentWeaponRange = currentWeapon.range;
        Transform barrel = equipmentManager.currentWeapon.transform.GetChild(0);
        ray.origin = barrel.position;
        ray.direction = target.position - barrel.position;

        if (Physics.Raycast(ray, out hit, currentWeaponRange, ~zombieLayer))
        {
            Debug.DrawRay(ray.origin, ray.direction, Color.red, 10); 
            if (hit.collider.gameObject.tag == "Enemy")
            {
                EnemyStats enemyStats = hit.collider.gameObject.GetComponentInParent<EnemyStats>();
                EnemyAI enemyAI = hit.collider.gameObject.GetComponentInParent<EnemyAI>();
                enemyAI.isHit = true;
                DealDamage(enemyStats, currentWeapon.damage);
            }

            if (hit.collider.gameObject.tag != "Enemy")
            {
                Instantiate(currentWeapon.hitParticles, hit.point, Quaternion.identity);
            }
        }
        Instantiate(currentWeapon.muzzleFlashParticles, barrel.position, Quaternion.identity);
    }

    private void Shoot(Weapon currentWeapon)
    {
        WeaponClass weaponScript = currentWeapon.prefab.GetComponent<WeaponClass>();
        weaponScript.CheckCanShoot();

        if (weaponScript.canShoot && weaponScript.canReload)
        {
            if (Time.time > lastFireTime + currentWeapon.fireRate)
            {
                lastFireTime = Time.time;
                RaycastShoot(currentWeapon);
                weaponScript.UseAmmo(1, 0);
                PlayAudio(currentWeapon.fireFX);
                StartCoroutine(ShootAnim());
            }
        }
    }

    private void StartAim()
    {
        aimLayer.weight += Time.deltaTime / aimDuration;
    }

    private void CancelAim()
    {
        aimLayer.weight -= Time.deltaTime / aimDuration;
    }

    private void PlayAudio(AudioClip clip)
    {
        gunAudio.clip = clip;
        gunAudio.Play();
    }

    public void DealDamage(CharacterStats statsToDamage, int damage)
    {
        statsToDamage.TakeDamage(damage);
    }

    IEnumerator ShootAnim()
    {
        animator.SetLayerWeight(animator.GetLayerIndex("Shoot Layer"), 1);
        animator.SetTrigger("Shoot");
        yield return new WaitForSeconds(0.9f);
        animator.SetLayerWeight(animator.GetLayerIndex("Shoot Layer"), 0);
    }

    private void GetReferences()
    {
        inventory = GetComponent<Inventory>();
        equipmentManager = GetComponent<EquipmentManager>();
        animator = GetComponent<Animator>();
        gunAudio = GetComponent<AudioSource>();
    }
}