using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IEnemyState
{
    EnemyAI enemyAI;
    private float lastAttackTime = 0;
    private bool hasReachedPlayer = false;

    public AttackState(EnemyAI enemy)
    {
        enemyAI = enemy;
    }

    public void UpdateState()
    {
        float distToTarget = Vector3.Distance(enemyAI.transform.position, enemyAI.target.transform.position);
        Vector3 lookDir = enemyAI.target.transform.position - enemyAI.transform.position;
        LookAtTarget(lookDir);

        if (enemyAI.scream)
        {
            if (!enemyAI.navMeshAgent.isStopped)
            {
                enemyAI.navMeshAgent.isStopped = true;
                enemyAI.animator.SetTrigger("Scream");
            }
        }
        else
        {
            enemyAI.navMeshAgent.isStopped = false;
            FollowTarget(distToTarget);
        }
        

        if(distToTarget >= enemyAI.viewDist)
        {
            StopFollow();
        }
    }

    private void FollowTarget(float distToTarget)
    {
        enemyAI.navMeshAgent.SetDestination(enemyAI.target.transform.position);
        enemyAI.animator.SetFloat("Speed", 1f, 0.3f, Time.deltaTime);
        enemyAI.navMeshAgent.speed = enemyAI.chaseSpeed;

        if (distToTarget <= enemyAI.navMeshAgent.stoppingDistance)
        {
            enemyAI.animator.SetFloat("Speed", 0f);

            if (!hasReachedPlayer)
            {
                hasReachedPlayer = true;
                lastAttackTime = Time.time;
            }

            if (Time.time >= lastAttackTime + enemyAI.GetComponent<EnemyStats>().attackSpeed)
            {
                lastAttackTime = Time.time;
                enemyAI.animator.SetTrigger("Attack");
            }
        }
        else
        {
            if (hasReachedPlayer)
            {
                hasReachedPlayer = false;
            }
        }
    }
    
    private void LookAtTarget(Vector3 lookDir)
    {
        enemyAI.transform.rotation = Quaternion.FromToRotation(Vector3.forward, new Vector3(lookDir.x, 0, lookDir.z));
    }

    private void StopFollow()
    {
        enemyAI.isAware = false;
        ToWanderState();
    }

    public void ToWanderState()
    {
        enemyAI.currentState = enemyAI.wanderState;
    }

    public void Impact()
    {

        enemyAI.animator.SetTrigger("Hit");
    }

    public void ToAlertState() { }
    public void ToAttackState() { }
    public void OnTriggerEnter(Collider col) { }
    public void OnTriggerStay(Collider col) { }
    public void OnTriggerExit(Collider col) { }
}
