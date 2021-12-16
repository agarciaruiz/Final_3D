using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyStats : CharacterStats
{
    [SerializeField] private Animator animator;
    [SerializeField] private ParticleSystem bloodParticles;
    [SerializeField] private int damage;
    private LootableObj lootableObj;
    public float attackSpeed = 1.5f;

    private void Start()
    {
        GetReferences();
        InitVariables();
    }

    public void DealDamage(CharacterStats statsToDamage)
    {
        if (statsToDamage != null)
        {
            statsToDamage.TakeDamage(damage);
        }
    }

    public override void Die()
    {
        base.Die();
        animator.SetTrigger("Die");
    }

    public void DestroyEnemy()
    {
        lootableObj.DropLoot();
        Destroy(this.gameObject);
    }

    public void EnableBloodParticles()
    {
        bloodParticles.Play();
    }

    public void DisableBloogParticles()
    {
        bloodParticles.Stop();
    }

    public override void InitVariables()
    {
        SetHealthTo(maxHealth);
        isDead = false;
    }

    private void GetReferences()
    {
        lootableObj = GetComponent<LootableObj>();
    }
}
