using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiTourelle : MonoBehaviour
{
    [SerializeField] AudioClip ennemiMeurt;
    bool tourner = true;
    public Animator animator;
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(bouger());
    }

  

    private IEnumerator DeclencherAttaque(){

        //animation
        
        animator.SetBool("isAttacking", true);
        yield return new WaitForSeconds(3.3f);
        //instantier boule
        GameObject boule = GameObject.Instantiate((GameObject)Resources.Load("BouleFeu"), new Vector3(transform.position.x, transform.position.y+5, transform.position.z), Quaternion.identity);
        boule.transform.SetParent(this.transform);
        boule.transform.rotation = Quaternion.identity;
        
       
        // yield return new WaitForSeconds(1f);
        rb = boule.GetComponent<Rigidbody>();
        // rb.AddForce(0,-0.2f,1f, ForceMode.Impulse);
        rb.AddForce(transform.up * 400f);
        rb.AddForce(transform.forward * 30f, ForceMode.Impulse);
        tourner = true;
        boule.transform.SetParent(null);
        StartCoroutine(bouger());
        animator.SetBool("isAttacking", false);
        


       
        yield return new WaitForSeconds(1f);
      


    }

    private IEnumerator bouger(){

    float startRotation = transform.eulerAngles.y;
    float endRotation = startRotation + 360.0f;
    float t = 0.0f;
    float duration = 10f;
    float delai = 0f;
    


    
     while ( tourner )
     {
        //  t += Time.deltaTime;
        //  float yRotation = Mathf.Lerp(startRotation, endRotation, t / duration) % 360.0f;
        //  transform.eulerAngles = new Vector3(transform.eulerAngles.x, yRotation, 
        //  transform.eulerAngles.z);

         transform.Rotate(new Vector3(0, -1, 0), 20f * Time.deltaTime);
         delai++;
         
           if(delai>300){
            Debug.Log("la toureelle doit arretter et lancer");
            tourner = false;
            delai = 0;
            StartCoroutine(DeclencherAttaque());
     }
         yield return null;
     }

   
        

        
   
  
  }

    // Update is called once per frame
    void Update()
    {
        
    }
}
