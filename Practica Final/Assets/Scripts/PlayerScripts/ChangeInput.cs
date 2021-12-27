using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Vehicles.Car;

public class ChangeInput : MonoBehaviour
{
    [Header("Player")]
    private GameObject player;
    private GameObject playerCams;

    [Header("Car")]
    private GameObject carCam;
    private CarUserControl carUserControl;
    private CarController carController;
    NavigationController navigationController;
    CarWaypointNavigator carWaypointNavigator;
    NavMeshAgent navMeshAgent;

    private bool canDrive = false;

    // Start is called before the first frame update
    void Start()
    {
        GetReferences();
        InitVariables();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canDrive)
        {
            GetIntoCar();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            GetOutOfCar();
        }
    }

    void GetOutOfCar()
    {
        // Disable driving
        carController.enabled = false;
        carUserControl.enabled = false;
        navigationController.enabled = true;
        carWaypointNavigator.enabled = true;
        navMeshAgent.enabled = true;


        // Enable player
        player.transform.parent = null;
        player.SetActive(true);

        // Swap cameras
        playerCams.SetActive(true);
        carCam.SetActive(false);
    }

    void GetIntoCar()
    {
        // Enable driving
        carController.enabled = true;
        carUserControl.enabled = true;
        navigationController.enabled = false;
        carWaypointNavigator.enabled = false;
        navMeshAgent.enabled = false;

        // Disable player
        player.transform.parent = this.gameObject.transform;
        player.SetActive(false);

        // Swap cameras
        playerCams.SetActive(false);
        carCam.SetActive(true);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            canDrive = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            canDrive = false;
        }
    }

    private void GetReferences()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerCams = GameObject.FindGameObjectWithTag("PlayerCam");

        carCam = transform.GetChild(0).gameObject;
        carController = GetComponent<CarController>();
        carUserControl = GetComponent<CarUserControl>();
        navigationController = GetComponent<NavigationController>();
        carWaypointNavigator = GetComponent<CarWaypointNavigator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void InitVariables()
    {
        carUserControl.enabled = false;
        carController.enabled = false;
    }
}
