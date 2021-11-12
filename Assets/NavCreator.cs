using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//pour aller cherche la librairie ui
using UnityEngine.AI;

public class NavCreator : MonoBehaviour
{
    //dire au nav mesh de creer sa surface
    public NavMeshSurface surface;
    
    void Start()
    {
        //pour creer le nav mesh
        GetComponent<NavMeshSurface>().BuildNavMesh();
        
    }


    void Update()
    {
        
    }
}
