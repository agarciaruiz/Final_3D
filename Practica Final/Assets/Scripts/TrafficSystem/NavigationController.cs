using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationController : MonoBehaviour
{
    private FOV fov;
    private Animator animator;
    private NavMeshAgent agent;
    private Vector3 destination;
    public bool reachedDestination;
    private float stopDistance = 2.5f;
    private bool runAway;

    void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        fov = GetComponent<FOV>();
    }

    void Update()
    {
        if (transform.position != destination)
        {
            float dist = Vector3.Distance(destination, transform.position);
            if (dist > stopDistance)
            {
                reachedDestination = false;
                agent.destination = destination;
            }
            else
            {
                reachedDestination = true;
                if (runAway)
                {
                    runAway = false;
                }
            }

            if (fov != null && fov.detected)
            {
                runAway = true;
                Vector3 destination = new Vector3(fov.visibleTargets[0].transform.position.x, fov.visibleTargets[0].transform.position.y, -fov.visibleTargets[0].transform.position.z);
                agent.ResetPath();
                SetDestination(destination);
                fov.detected = false;
            }

            if (animator != null)
            {
                if (runAway)
                {
                    animator.SetFloat("Speed", 1);
                    agent.speed = 3;
                }
                else
                {
                    animator.SetFloat("Speed", 0.5f);
                    agent.speed = 1;
                }
            }
        }
    }

    public void SetDestination(Vector3 destination)
    {
        this.destination = destination;
        reachedDestination = false;
    }
}