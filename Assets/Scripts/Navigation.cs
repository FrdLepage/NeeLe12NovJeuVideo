using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Navigation : MonoBehaviour
{
    
    public void StartLeJeu(){
       SceneManager.LoadScene("Ile"); 
    }
    public void QuitterLejeu(){
       Application.Quit();
    }
    

}
