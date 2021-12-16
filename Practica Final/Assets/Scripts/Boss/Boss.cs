using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private BossArea bossArea;
    //[SerializeField] private float speed;
    [SerializeField] private GameObject[] hitColliders;
    [SerializeField] private GameObject bossCanvas;
    [SerializeField] private GameManager gameManager;

    [HideInInspector] public BossStats bossStats;
    [HideInInspector] public GameObject target;
    [HideInInspector] public bool isAttacking;
    [HideInInspector] public int collSelect;

    private int routine;
    private float timer = 0;
    private float routineTimer = 2;
    private Animator animator;
    private float distToPlayer = 15;
    private float walkSpeed = 2;
    private float runSpeed = 4;

    // Flame thrower
    [SerializeField] private GameObject fireSphere;
    [SerializeField] private GameObject head;
    [SerializeField] private ParticleSystem flameParticles;
    private bool flameThrower;
    private List<GameObject> pool = new List<GameObject>();
    private float timer2;
    private float flameThrowerSkill = 0.8f;

    // Jump Attack
    private float jumpDist;
    private bool dirSkill;
    private float jumpSpeed = 8;
    private float jumpAttackSkill = 0.6f;

    // Fireball
    [SerializeField] private GameObject fireball;
    [SerializeField] private GameObject point;
    private List<GameObject> pool2 = new List<GameObject>();
    private float fireballSkill = 1;

    // Phases
    [HideInInspector] public int phase = 1;

    void Start()
    {
        bossStats = GetComponent<BossStats>();
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player");
    }

    public void BossBehavior()
    {
        if(Vector3.Distance(transform.position, target.transform.position) < distToPlayer)
        {
            bossCanvas.SetActive(true);
            Vector3 lookDir = target.transform.position - transform.position;
            lookDir.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookDir);
            point.transform.LookAt(target.transform.position);

            if(Vector3.Distance(transform.position, target.transform.position) > 1 && !isAttacking)
            {
                switch (routine)
                {
                    case 0:
                        // Walk
                        animator.SetBool("Walk", true);
                        animator.SetBool("Run", false);
                        MoveToPlayer(rotation, walkSpeed);

                        timer += 1 * Time.deltaTime;
                        if(timer > routineTimer)
                        {
                            routine = Random.Range(0, 5);
                            timer = 0;
                        }

                        break;

                    case 1:
                        // Run
                        animator.SetBool("Walk", false);
                        animator.SetBool("Run", true);
                        MoveToPlayer(rotation, runSpeed);
                        break;

                    case 2:
                        // Flamethrow
                        SetAnimatorForAttack(flameThrowerSkill);
                        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
                        bossArea.GetComponent<CapsuleCollider>().enabled = false;
                        break;

                    case 3:
                        //Jump Attack
                        if(phase == 2)
                        {
                            jumpDist += 1 * Time.deltaTime;
                            SetAnimatorForAttack(jumpAttackSkill);
                            collSelect = 3;
                            bossArea.GetComponent<CapsuleCollider>().enabled = false;

                            if (dirSkill)
                            {
                                if(jumpDist < 1)
                                {
                                    transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
                                }

                                transform.Translate(Vector3.forward * jumpSpeed * Time.deltaTime);
                            }
                        }
                        else
                        {
                            routine = 0;
                            timer = 0;
                        }
                        break;

                    case 4:
                        // Fireball
                        if(phase == 2)
                        {
                            SetAnimatorForAttack(fireballSkill);
                            bossArea.GetComponent<CapsuleCollider>().enabled = false;
                            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 0.5f);
                        }
                        else
                        {
                            routine = 0;
                            timer = 0;
                        }
                        break;
                }
            }
        }
    }

    private void MoveToPlayer(Quaternion rotation, float speed)
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);

        if (transform.rotation == rotation)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        animator.SetBool("Attack", false);
    }

    private void SetAnimatorForAttack(float skill)
    {
        animator.SetBool("Walk", false);
        animator.SetBool("Run", false);
        animator.SetBool("Attack", true);
        animator.SetFloat("Skills", skill);
    }

    public void EndAnimations()
    {
        routine = 0;
        animator.SetBool("Attack", false);
        isAttacking = false;
        bossArea.GetComponent<CapsuleCollider>().enabled = true;
        flameThrower = false;
        jumpDist = 0;
        dirSkill = false;
    }

    public void EnableAttackDir()
    {
        dirSkill = true;
    }

    public void DisableAttackDir()
    {
        dirSkill = false;
    }

    public void EnableHitCollider()
    {
        hitColliders[collSelect].GetComponent<SphereCollider>().enabled = true;
    }

    public void DisableHitCollider()
    {
        hitColliders[collSelect].GetComponent<SphereCollider>().enabled = false;
    }

    //Firethrow
    public GameObject GetFlamethrow()
    {
        for(int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                pool[i].SetActive(true);
                return pool[i];
            }
        }

        GameObject obj = (GameObject)Instantiate(fireSphere, head.transform);
        pool.Add(obj);
        return obj;
    }

    public void FlamethrowSkill()
    {
        timer2 += 1 * Time.deltaTime;
        if(timer2 > 0.1f)
        {
            GameObject obj = GetFlamethrow();
            obj.transform.position = head.transform.position;
            obj.transform.rotation = head.transform.rotation;
            timer2 = 0;
        }
    }

    public void StartFire()
    {
        flameThrower = true;
    }

    public void StopFire()
    {
        flameThrower = false;   
    }

    //Fireball
    public GameObject GetFireball()
    {
        for (int i = 0; i < pool2.Count; i++)
        {
            if (!pool2[i].activeInHierarchy)
            {
                pool2[i].SetActive(true);
                return pool2[i];
            }
        }

        GameObject obj = (GameObject)Instantiate(fireball, point.transform);
        pool2.Add(obj);
        return obj;
    }

    public void FireballSkill()
    {
        GameObject obj = GetFireball();
        obj.transform.position = point.transform.position;
        obj.transform.rotation = point.transform.rotation;
    }

    public void BossAlive()
    {
        if(bossStats.health < (bossStats.maxHealth / 2))
        {
            phase = 2;
            routineTimer = 1;
        }

        BossBehavior();

        if (flameThrower)   
        {
            FlamethrowSkill();
        }
    }

    public void PlayFlameParticles()
    {
        flameParticles.Play();
    }

    public void StopFlameParticles()
    {
        flameParticles.Stop();
    }

    private void Update()
    {
        if(!bossStats.isDead)
        {
            BossAlive();
        }
        else
        {
            gameManager.bossDefeted = true;
        }
    }

    public void EndDeadAnim()
    {
        gameManager.EndGame();
    }
}
