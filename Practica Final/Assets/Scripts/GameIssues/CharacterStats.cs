using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public float health;
    public float maxHealth;

    [HideInInspector] public float shield;
    [HideInInspector] public float maxShield;

    [SerializeField] public bool isDead;

    private void Start()
    {
        InitVariables();
    }

    public virtual void CheckHealth()
    {
        if(health <= 0)
        {
            health = 0;
            Die();
        }
        if(health >= maxHealth)
        {
            health = maxHealth;
        }
    }

    public virtual void CheckShield()
    {
        if (shield <= 0)
        {
            shield = 0;
        }
        if (shield >= maxShield)
        {
            shield = maxShield;
        }
    }

    public virtual void Die()
    {
        isDead = true;
    }

    public void SetHealthTo(float healthToSet)
    {
        health = healthToSet;
        CheckHealth();
    }

    public void SetShieldTo(float shieldToSet)
    {
        shield = shieldToSet;
        CheckShield();
    }

    public virtual void TakeDamage(int dmg)
    {
        if(shield != 0)
        {
            float newShield = shield - (dmg * 0.9f);
            float newHealth = health - (dmg*0.1f);
            SetShieldTo(newShield);
            SetHealthTo(newHealth);
        }
        else
        {
            float newHealth = health - dmg;
            SetHealthTo(newHealth);
        }
    }

    public void Heal(int heal)
    {
        float newHealth = health + heal;
        SetHealthTo(newHealth);
    }

    public void AddShield(int shieldToAdd)
    {
        float newShield = shield + shieldToAdd;
        SetShieldTo(newShield);
    }

    public virtual void InitVariables()
    {
        maxHealth = 100;
        maxShield = 100;
        SetHealthTo(maxHealth);
        SetShieldTo(maxShield);
        isDead = false;
    }
}
