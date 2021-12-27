using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunOver : MonoBehaviour
{
    [SerializeField] private ParticleSystem bloodParticles;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Citizen") || other.gameObject.CompareTag("Zombie"))
        {
            bloodParticles.Play();
            Destroy(other.gameObject);
        }
    }
}
