using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHairTarget : MonoBehaviour
{
    private Camera cam;
    Ray ray;
    RaycastHit hit;
    [SerializeField] private LayerMask zombieLayer;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        ray.origin = cam.transform.position;
        ray.direction = cam.transform.forward;
        Physics.Raycast(ray, out hit, Mathf.Infinity, ~zombieLayer);
        transform.position = hit.point;
    }
}
