using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunOver : MonoBehaviour
{
    [SerializeField] private ParticleSystem bloodParticles;
    private ZombieSpawner zombieSpawner;

    private void Start()
    {
        zombieSpawner = FindObjectOfType<ZombieSpawner>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Citizen"))
        {
            bloodParticles.Play();
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Zombie"))
        {
            zombieSpawner.enemiesKilled++;
            bloodParticles.Play();
            Destroy(other.gameObject);
        }
    }
}
