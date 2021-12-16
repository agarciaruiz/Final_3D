using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossArea : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Boss boss;
    [HideInInspector] public int melee;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            melee = Random.Range(0, 4);
            switch (melee)
            {
                case 0:
                    // Attack 1
                    animator.SetFloat("Skills", 0);
                    boss.collSelect = 0;
                    break;
                case 1:
                    //Attack 2
                    animator.SetFloat("Skills", 0.2f);
                    boss.collSelect = 1;
                    break;
                case 2:
                    // Jump
                    animator.SetFloat("Skills", 0.4f);
                    boss.collSelect = 2;
                    break;
                case 3:
                    // Fireball
                    if (boss.phase == 2)
                    {
                        animator.SetFloat("Skills", 1);
                    }
                    else
                    {
                        melee = 0;
                    }
                    break;
            }

            animator.SetBool("Walk", false);
            animator.SetBool("Run", false);
            animator.SetBool("Attack", true);
            boss.isAttacking = true;
            GetComponent<CapsuleCollider>().enabled = false;
        }
    }
}
