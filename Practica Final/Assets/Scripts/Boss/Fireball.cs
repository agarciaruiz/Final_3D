using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [HideInInspector] public float timer;
    private float speed = 15;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > 3)
        {
            gameObject.SetActive(false);
            timer = 0;
        }
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
