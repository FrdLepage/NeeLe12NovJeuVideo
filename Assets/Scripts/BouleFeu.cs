using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouleFeu : MonoBehaviour
{
    float speed = 20f;
    // Start is called before the first frame update
    void Start()
    {
        
    }
     void OnTriggerEnter(Collider other)
    {
        Debug.Log("collision avec le perso, doit perdre une vie");
    }

    // Update is called once per frame
    void Update()
    {
        // transform.position += Vector3.forward * Time.deltaTime * speed;

        // transform.position += transform.TransformDirection(Vector3.forward)*Time.deltaTime*speed;

        transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.World);

  
    }
}
