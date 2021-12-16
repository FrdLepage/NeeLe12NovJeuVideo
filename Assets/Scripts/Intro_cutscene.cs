using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro_cutscene : MonoBehaviour
{
    public float compteur;
    
    public void changerScene()
    {
        SceneManager.LoadScene("Start");
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        compteur += Time.deltaTime;
        if(compteur>=35){
            SceneManager.LoadScene("Start");
        }
    }
}
