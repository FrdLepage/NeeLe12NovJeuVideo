using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouleFeu : MonoBehaviour
{
    /// <summary>
    /// Permet la destruction de la boule quand elle entre en collision avec
    /// un cube au sol
    /// </summary>
    /// <param name="other">collider</param>
    private void OnTriggerEnter(Collider other)
    {
        //si collision avec l'objet portant le tag Cube
        if(other.tag == "Cube"){
           Destroy(gameObject); //destruction de l'objet
        }
     
    }
}
