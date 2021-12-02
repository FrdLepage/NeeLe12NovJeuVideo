using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartSystem : MonoBehaviour
{
    public GameObject[] hearts;
    private int life;
    private bool dead;
    // Start is called before the first frame update
    private void Start() {
        life=hearts.Length;
    }
    void Update() {
        if (dead==true){
            Debug.Log("Personnage mort");
        }
    }

    public void TakeDamage(int d){
        if (life>=1)
        {
            life -= d;
            Destroy(hearts[life].gameObject);
            if(life < 1)
            {
                dead=true;
            }
        }
    }
}
