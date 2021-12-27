using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarWaypointNavigator : MonoBehaviour
{
    private NavigationController navigationController;
    public Waypoint currentWaypoint;

    private void Awake()
    {
        navigationController = GetComponent<NavigationController>();
    }

    private void Start()
    {
        navigationController.SetDestination(currentWaypoint.GetPosition());
    }

    void Update()
    {
        if (navigationController.reachedDestination)
        {
            bool shouldBranch = false;

            if (currentWaypoint.branches != null && currentWaypoint.branches.Count > 0)
            {
                shouldBranch = Random.Range(0f, 1f) <= currentWaypoint.branchRatio ? true : false;
            }

            if (shouldBranch)
            {
                currentWaypoint = currentWaypoint.branches[Random.Range(0, currentWaypoint.branches.Count - 1)];
            }
            else
            {
                if (currentWaypoint.nextWaypoint != null)
                {
                    currentWaypoint = currentWaypoint.nextWaypoint;
                    navigationController.SetDestination(currentWaypoint.GetPosition());
                }
            }
        }
    }
}
