using UnityEngine;

public interface IEnemyState
{
    void UpdateState();
    void ToAlertState();
    void ToWanderState();
    void ToAttackState();

    void OnTriggerEnter(Collider col);
    void OnTriggerStay(Collider col);
    void OnTriggerExit(Collider col);

    void Impact();
}
