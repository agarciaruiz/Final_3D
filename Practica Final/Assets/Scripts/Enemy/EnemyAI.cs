using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [HideInInspector] public WanderState wanderState;
    [HideInInspector] public AlertState alertState ;
    [HideInInspector] public AttackState attackState ;
    [HideInInspector] public IEnemyState currentState;

    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public Animator animator;
    [HideInInspector] public GameObject target;

    [HideInInspector] public EnemyStats enemyStats;

    [HideInInspector] public float rotationTime = 10.0f;
    [HideInInspector] public float shootHeight = 0.5f;
    [HideInInspector] public float viewDist = 20f;
    [HideInInspector] public float fov = 120;
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
                currentState.Impact();
                isHit = false;
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

    public void MoveAgent()
    {
        //navMeshAgent.isStopped = false;
        animator.SetLayerWeight(animator.GetLayerIndex("Hit Layer"), 0);
    }

    public void StopAgent()
    {
        //navMeshAgent.isStopped = true;
        animator.SetLayerWeight(animator.GetLayerIndex("Hit Layer"), 1);
    }

    private void InitVariables()
    {
        isHit = false;
        currentState = wanderState;
    }

    private void GetReferences()
    {
        wanderState = new WanderState(this);
        alertState = new AlertState(this);
        attackState = new AttackState(this);

        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player");
        enemyStats = GetComponent<EnemyStats>();
    }
}
