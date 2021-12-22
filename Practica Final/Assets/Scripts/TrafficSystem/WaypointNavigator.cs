using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointNavigator : MonoBehaviour
{
    private NavigationController navigationController;
    public Waypoint currentWaypoint;

    private void Awake()
    {
        navigationController = GetComponent<NavigationController>();
    }

    void Start()
    {
        navigationController.SetDestination(currentWaypoint.GetPosition());
    }

    void Update()
    {
        if (navigationController.reachedDestination)
        {
            currentWaypoint = currentWaypoint.nextWaypoint;
            navigationController.SetDestination(currentWaypoint.GetPosition());
        }
    }
}
