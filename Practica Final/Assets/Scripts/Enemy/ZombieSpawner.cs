using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private GameObject agentPrefab;
    [SerializeField] private GameObject zombiesParent;

    private GameObject[] spawners;
    private int waveNumber = 0;
    private int spawnAmount = 0;

    [HideInInspector] public int enemiesKilled = 0;

    private void Start()
    {
        spawners = new GameObject[transform.childCount];
        for(int i = 0; i<transform.childCount; i++)
        {
            spawners[i] = transform.GetChild(i).gameObject;
        }

        StartWave();
    }

    private void Update()
    {
        if(enemiesKilled >= spawnAmount)
        {
            NextWave();
        }
    }

    private void Spawn()
    {
        int spawnerID = Random.Range(0, spawners.Length);
        GameObject instance = Instantiate(agentPrefab, spawners[spawnerID].transform.position, Quaternion.identity);
        instance.transform.parent = zombiesParent.transform;
    }

    private void StartWave()
    {
        waveNumber = 1;
        spawnAmount = 5;
        enemiesKilled = 0;

        for(int i = 0; i < spawnAmount; i++)
        {
            Spawn();
        }
    }

    private void NextWave()
    {
        waveNumber++;
        spawnAmount += 5;
        enemiesKilled = 0;

        for (int i = 0; i < spawnAmount; i++)
        {
            Spawn();
        }
    }
}
