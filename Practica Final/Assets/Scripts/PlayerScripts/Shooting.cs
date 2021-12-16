using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations.Rigging;

public class Shooting : MonoBehaviour
{
    [SerializeField] private Rig aimLayer;
    [SerializeField] private float aimDuration = 0.1f;
    [SerializeField] private Transform target;

    private Inventory inventory;
    private EquipmentManager equipmentManager;
    private Animator animator;
    private AudioSource gunAudio;
    [HideInInspector] public float soundIntensity = 100f;
    [SerializeField] private LayerMask zombieLayer;

    private PlayerInput playerInput;
    private InputAction reloadAction;

    private float lastFireTime;

    private void Awake()
    {
        GetReferences();
        InitVariables();
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

        if (reloadAction.triggered)
        {
            WeaponClass weaponClass = GameObject.FindGameObjectWithTag("Weapon").GetComponent<WeaponClass>();
            weaponClass.Reload(equipmentManager.currentlyEquipedWeapon);
            PlayAudio(weaponClass.weapon.reloadFX);
        }
    }

    private void RaycastShoot(Weapon currentWeapon, WeaponClass currentWeaponClass)
    {
        Ray ray = new Ray();
        RaycastHit hit;

        float currentWeaponRange = currentWeapon.range;
        Transform barrel = currentWeaponClass.weaponBarrel;
        ray.origin = transform.TransformPoint(barrel.position);
        ray.direction = target.position - transform.TransformPoint(barrel.position);

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
                //EmitParticles(currentWeapon.hitParticles, hit.point, hit.normal);
            }
        }
        //currentWeapon.muzzleFlashParticles.Emit(1);
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
                RaycastShoot(currentWeapon, weaponScript);
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

    private void EmitParticles(ParticleSystem particles, Vector3 point, Vector3 normal)
    {
        particles.transform.position = point;
        particles.transform.forward = normal;
        particles.Emit(1);
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
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
        gunAudio = GetComponent<AudioSource>();
    }

    private void InitVariables()
    {
        reloadAction = playerInput.actions["Reload"];
    }
}
