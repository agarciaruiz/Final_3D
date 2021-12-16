using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUI : MonoBehaviour
{
    [SerializeField] private ProgressBar healthBar;

    public void UpdateHealth(float currentHealth, float maxHealth)
    {
        healthBar.SetValues(currentHealth, maxHealth);
    }
}
