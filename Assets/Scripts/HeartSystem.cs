using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeartSystem : MonoBehaviour
{
    [SerializeField] AudioClip persoDegat;//audio du dega
    public GameObject[] hearts;//tableau de image de vie
    private int life;//nombre de vie
    private bool dead;//est-ce que le personnage meurt
    // Start is called before the first frame update
    private void Start() {
        life=hearts.Length;//la vie est égale au nombre de gameobject dans le tableau
    }
    void Update() {
        if (dead==true){//si le personnage est mort amène le à la scene de mort
            Debug.Log("Personnage mort");
            SceneManager.LoadScene("Perdu");

        }
    }

    
    public void TakeDamage(int d){//prendre du dommange
        if (life>=1)//si le personnage a plus d'une vie enleve une vie et joue le son de dommage
        {
            life -= d;//enleve un certain nombre de vie dans le parametre
            SoundManager.instance.JouerSon(persoDegat);//joue le son
            Destroy(hearts[life].gameObject);//detruit le gameObject
            if(life < 1)//si le personnage a moin d'une vie il meurt
            {
                dead=true;
            }
        }
    }
}
