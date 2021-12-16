using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    [SerializeField] private EnemyStats enemyStats;
    [SerializeField] private PlayerStats playerStats;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!other.GetComponent<PlayerStats>().isDead)
            {
                enemyStats.DealDamage(playerStats);
            }
        }
    }
}
