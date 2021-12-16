using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertState: IEnemyState
{
    EnemyAI enemyAI;
    float currentRotationTime = 0;

    public AlertState(EnemyAI enemy)
    {
        enemyAI = enemy;
    }

    public void UpdateState()
    {
        enemyAI.transform.rotation *= Quaternion.Euler(0f, Time.deltaTime * 360 * 1.0f / enemyAI.rotationTime, 0f);

        if(currentRotationTime > enemyAI.rotationTime)
        {
            enemyAI.animator.SetBool("Turn", false);
            currentRotationTime = 0;
            ToWanderState();
        }
        else
        {
            enemyAI.animator.SetBool("Turn", true);
            RaycastHit hit;

            if(Physics.Raycast(new Ray(
                new Vector3(enemyAI.transform.position.x, enemyAI.transform.position.y + 1, enemyAI.transform.position.z),
                enemyAI.transform.forward * 100),
                out hit))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    enemyAI.animator.SetBool("Turn", false);
                    enemyAI.animator.SetTrigger("Scream");
                    ToAttackState();
                }
            }

            currentRotationTime += Time.deltaTime;
        }
    }

    public void ToAlertState() {}
    public void ToAttackState()
    {
        Debug.Log("Attack State");
        enemyAI.currentState = enemyAI.attackState;
    }

    public void ToWanderState()
    {
        enemyAI.navMeshAgent.isStopped = false;
        enemyAI.currentState = enemyAI.wanderState;
    }

    public void Impact()
    {
        enemyAI.animator.SetBool("Turn", false);
        enemyAI.animator.SetTrigger("Scream");
        ToAttackState();
    }

    public void OnTriggerEnter(Collider col) {}
    public void OnTriggerStay(Collider col) {}
    public void OnTriggerExit(Collider col) {}
}
