using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOV : MonoBehaviour
{
    [SerializeField] LayerMask obstacleMask;
    [SerializeField] LayerMask zombieMask;

    private int view_angle = 45;

    [HideInInspector] public List<Transform> visibleTargets = new List<Transform>();
    [HideInInspector] public int viewDist = 10;
    [HideInInspector] public bool detected;

    private void Start()
    {
        StartCoroutine(FindTargets(.2f));
    }

    IEnumerator FindTargets(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            DetectEnemies();
        }
    }

    private void DetectEnemies()
    {
        Collider[] visibleTarget = Physics.OverlapSphere(transform.position, viewDist, zombieMask);

        for(int i = 0; i < visibleTarget.Length; i++)
        {
            Transform target = visibleTarget[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;

            if(Vector3.Angle(transform.forward, dirToTarget) < view_angle / 2)
            {
                float distToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, dirToTarget, distToTarget, obstacleMask))
                {
                    visibleTargets.Add(target);
                    detected = true;
                }
            }
        }
    }
}
