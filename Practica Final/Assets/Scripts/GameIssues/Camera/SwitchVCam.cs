using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class SwitchVCam : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private int priorityAmount = 10;
    [SerializeField] private Canvas thirdPersonCanvas;
    [SerializeField] private Canvas aimCanvas;

    private CinemachineVirtualCamera vCam;

    private void Awake()
    {
        vCam = GetComponent<CinemachineVirtualCamera>();
        aimCanvas.enabled = false;
    }

    private void Update()
    {
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            StartAim();
        }

        if (Mouse.current.rightButton.wasReleasedThisFrame)
        {
            CancelAim();
        }
    }

    private void StartAim()
    {
        vCam.Priority += priorityAmount;
        aimCanvas.enabled = true;
        thirdPersonCanvas.enabled = false;
    }

    private void CancelAim()
    {
        vCam.Priority -= priorityAmount;
        aimCanvas.enabled = false;
        thirdPersonCanvas.enabled = true;
    }
}
