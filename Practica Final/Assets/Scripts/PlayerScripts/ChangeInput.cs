using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    private bool canDrive = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerCams = GameObject.FindGameObjectWithTag("PlayerCam");

        carCam = transform.GetChild(0).gameObject;
        carController = GetComponent<CarController>();
        carUserControl = GetComponent<CarUserControl>();
        carUserControl.enabled = false;
        carController.enabled = false;
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
}
