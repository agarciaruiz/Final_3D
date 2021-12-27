using UnityEngine;
using UnityEngine.AI;

public class WanderState : IEnemyState
{
    EnemyAI enemyAI;
    private float wanderRadius = 50;

    public WanderState(EnemyAI enemy)
    {
        enemyAI = enemy;
    }

    public void UpdateState()
    {
        if (enemyAI.isAware)
        {
            enemyAI.scream = true;
            ToAttackState();
        }
        else
        {
            Wander();
            enemyAI.navMeshAgent.speed = enemyAI.wanderSpeed;
            enemyAI.animator.SetFloat("Speed", 0.5f);
            SearchForPlayer();
        }
    }

    private void SearchForPlayer()
    {
        if (enemyAI.fov.detected)
        {
            enemyAI.OnAware();
        }
    }

    private void Wander()
    {
        Vector3 wanderPoint = RandomWanderPoint();

        if (enemyAI.navMeshAgent != null && enemyAI.navMeshAgent.remainingDistance <= enemyAI.navMeshAgent.stoppingDistance)
        {
            enemyAI.navMeshAgent.destination = wanderPoint;
        }
    }

    private Vector3 RandomWanderPoint()
    {
        Vector3 randomPoint = (Random.insideUnitSphere * wanderRadius) + enemyAI.transform.position;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomPoint, out navHit, wanderRadius, -1);
        return new Vector3(navHit.position.x, enemyAI.transform.position.y, navHit.position.z);
    }

    public void Impact()
    {
        enemyAI.animator.SetTrigger("Hit");
        enemyAI.OnAware();
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            enemyAI.OnAware();
        }
    }

    public void ToAttackState()
    {
        enemyAI.currentState = enemyAI.attackState;
    }

    public void ToAlertState(){}
    public void ToWanderState(){}
    public void OnTriggerStay(Collider col){}
    public void OnTriggerExit(Collider col) {}
}
