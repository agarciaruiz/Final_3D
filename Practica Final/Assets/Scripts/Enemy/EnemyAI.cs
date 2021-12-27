using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private AudioClip screamFX;

    [HideInInspector] public WanderState wanderState;
    [HideInInspector] public AttackState attackState;
    [HideInInspector] public IEnemyState currentState;

    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public Animator animator;
    [HideInInspector] public AudioSource audioSource;
    [HideInInspector] public GameObject target;

    [HideInInspector] public EnemyStats enemyStats;
    [HideInInspector] public FOV fov;

    [HideInInspector] public bool isAware = false;
    [HideInInspector] public bool isHit;
    [HideInInspector] public bool scream = false;
    [HideInInspector] public float wanderSpeed = 0.4f;
    [HideInInspector] public float chaseSpeed = 1.5f;

    void Start()
    {
        GetReferences();
        InitVariables();
    }

    void Update()
    {
        if (!enemyStats.isDead)
        {
            currentState.UpdateState();

            if (isHit)
            {
                isHit = false;
                currentState.Impact();
            }
        }
        else
        {
            navMeshAgent.isStopped = true;
        }
    }

    public void OnAware()
    {
        isAware = true;
    }

    public void PlayScreamSound()
    {
        audioSource.clip = screamFX;
        audioSource.Play();
    }

    public void StopScreaming()
    {
        scream = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter(other);
    }

    private void OnTriggerStay(Collider other)
    {
        currentState.OnTriggerStay(other);
    }

    private void OnTriggerExit(Collider other)
    {
        currentState.OnTriggerExit(other);
    }

    private void InitVariables()
    {
        isHit = false;
        currentState = wanderState;
    }

    private void GetReferences()
    {
        wanderState = new WanderState(this);
        attackState = new AttackState(this);

        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        target = GameObject.FindGameObjectWithTag("Player");
        enemyStats = GetComponent<EnemyStats>();
        fov = GetComponent<FOV>();
    }
}
