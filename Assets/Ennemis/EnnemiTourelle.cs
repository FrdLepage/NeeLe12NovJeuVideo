using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiTourelle : MonoBehaviour
{
    bool tourner = true;
    public Animator animator;
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(bouger());
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "objet"){
            // Debug.Log("la toureelle est rentrer en contact avec un biome transformer");
            tourner = false;
            StartCoroutine(DeclencherAttaque());

            
        }
    }

    private IEnumerator DeclencherAttaque(){

        //animation
        animator = GetComponent<Animator>();
        animator.SetBool("isAttacking", true);
        yield return new WaitForSeconds(2.8f);
        //instantier boule
        GameObject boule = GameObject.Instantiate((GameObject)Resources.Load("BouleFeu"), new Vector3(transform.position.x, transform.position.y+2, transform.position.z), Quaternion.identity);
        yield return new WaitForSeconds(1f);
        rb = boule.GetComponent<Rigidbody>();
        rb.AddForce(0,-0.4f,10f, ForceMode.Impulse);
        yield return new WaitForSeconds(1f);
        //lancer boule

    }

    private IEnumerator bouger(){

    float startRotation = transform.eulerAngles.y;
    float endRotation = startRotation + 360.0f;
    float t = 0.0f;
    float duration = 10f;
    


    
     while ( tourner )
     {
        //  t += Time.deltaTime;
        //  float yRotation = Mathf.Lerp(startRotation, endRotation, t / duration) % 360.0f;
        //  transform.eulerAngles = new Vector3(transform.eulerAngles.x, yRotation, 
        //  transform.eulerAngles.z);

         transform.Rotate(new Vector3(0, -1, 0), 20f * Time.deltaTime);

        
      
         yield return null;
     }
        

        
   
  
  }

    // Update is called once per frame
    void Update()
    {
        
    }
}
