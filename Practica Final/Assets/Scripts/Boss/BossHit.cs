using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHit : MonoBehaviour
{
    public int damage; 
    private Boss boss;

    private void Start()
    {
        boss = GetComponentInParent<Boss>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!other.GetComponent<PlayerStats>().isDead)
            {
                boss.bossStats.DealDamage(boss.target.GetComponent<PlayerStats>(), damage);
            }
        }
    }
}
