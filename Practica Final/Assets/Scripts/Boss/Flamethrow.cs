using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrow : MonoBehaviour
{
    private float timer;
    private float fireSpeed = 6;

    void Update()
    {
        transform.Translate(Vector3.forward * fireSpeed * Time.deltaTime);
        transform.localScale += new Vector3(3, 3, 3) * Time.deltaTime;

        timer += Time.deltaTime;

        if(timer > 1f)
        {
            transform.localScale = new Vector3(1, 1, 1);
            gameObject.SetActive(false);
            timer = 0;
        }
    }
}
