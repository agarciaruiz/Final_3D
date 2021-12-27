using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenSpawner : MonoBehaviour
{
    [SerializeField] private GameObject citizenPrefab;
    [SerializeField] private Transform parent;
    [SerializeField] private int citizenToSpawn;

    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        int count = 0;
        while(count < citizenToSpawn)
        {
            Transform child = transform.GetChild(Random.Range(0, transform.childCount - 1));
            GameObject obj = Instantiate(citizenPrefab, child.position, Quaternion.identity);
            obj.GetComponent<WaypointNavigator>().currentWaypoint = child.GetComponent<Waypoint>();
            //obj.transform.position = child.position;
            obj.transform.parent = parent;

            yield return new WaitForEndOfFrame();
            count++;
        }
    }
}
