using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoueurMiniMap : MonoBehaviour
{
    public Transform player;
    
    private void LateUpdate()
    {
        Vector3 newPosition = player.position;//prendre la position du jouer
        newPosition.y = transform.position.y;//prendre la position en y 
        transform.position = newPosition;//Deplace la camera en y

        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y , 0f);//si le personnage rotate rotate avec la camera
    }
}
