using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SmartDetection : MonoBehaviour
{
    [SerializeField] NavMeshAgent navMeshAgent;

    private void Start()
    {
        navMeshAgent = GetComponentInParent<NavMeshAgent>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Citizen" || other.gameObject.tag == "Car")
        {
            if (navMeshAgent != null)
            {
                navMeshAgent.isStopped = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Citizen" || other.gameObject.tag == "Car")
        {
            if (navMeshAgent != null)
            {
                navMeshAgent.isStopped = false;
            }
        }
    }
}
