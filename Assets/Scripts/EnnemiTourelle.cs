using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiTourelle : MonoBehaviour
{
    [SerializeField] AudioClip ennemiMeurt;
    bool tourner = true;
    public GameObject boule;
    public Transform target;
    public Animator animator;
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(bouger());
    }

     // Throws ball at location with regards to gravity (assuming no obstacles in path) and initialVelocity (how hard to throw the ball)
    public void ThrowBallAtTargetLocation(Vector3 targetLocation, float initialVelocity)
    {
        Vector3 direction = (targetLocation - transform.position).normalized;
        float distance = Vector3.Distance(targetLocation, transform.position);

        Debug.Log("physic.gravity..." + distance);
        Debug.Log("initialVelocity..." + initialVelocity);
        float firingElevationAngle = FiringElevationAngle(Physics.gravity.magnitude, distance, initialVelocity);
        Vector3 elevation = Quaternion.AngleAxis(firingElevationAngle, transform.right) * transform.up;
        float directionAngle = AngleBetweenAboutAxis(transform.forward, direction, transform.up);
        Vector3 velocity = Quaternion.AngleAxis(directionAngle, transform.up) * elevation * initialVelocity;

        Debug.Log("direction" + direction + "distance" + distance + "firingElevation" + firingElevationAngle + "elevation" + elevation + "directionAngle" + directionAngle + "transformPos" + transform.position);
        rb = boule.GetComponent<Rigidbody>();
        // ballGameObject is object to be thrown
        rb.AddForce(velocity, ForceMode.VelocityChange);
    }

    // Helper method to find angle between two points (v1 & v2) with respect to axis n
    public static float AngleBetweenAboutAxis(Vector3 v1, Vector3 v2, Vector3 n)
    {
        return Mathf.Atan2(
            Vector3.Dot(n, Vector3.Cross(v1, v2)),
            Vector3.Dot(v1, v2)) * Mathf.Rad2Deg;
    }

    // Helper method to find angle of elevation (ballistic trajectory) required to reach distance with initialVelocity
    // Does not take wind resistance into consideration.
    private float FiringElevationAngle(float gravity, float distance, float initialVelocity)
    {
        Debug.Log("formule fire elevation gravity" + gravity);
        Debug.Log("formule fire elevation distance" + distance);
        Debug.Log("formule fire elevation initialVelocity" + initialVelocity);
        float angle1 = Mathf.Asin((180f));
        float angle2 = 0.5f * Mathf.Asin((gravity * distance) / (initialVelocity * initialVelocity));
        float angle = 0.5f * Mathf.Asin((gravity * distance) / (initialVelocity * initialVelocity)) * Mathf.Rad2Deg;
       
        Debug.Log("formule fire elevation angle" + angle1 +angle2 + angle);
        return angle;
    }


  

    private IEnumerator DeclencherAttaque(){

        //animation
  
        animator.SetBool("isAttacking", true);
        yield return new WaitForSeconds(3f);
        //instantier boule
        boule = GameObject.Instantiate((GameObject)Resources.Load("BouleFeu"), new Vector3(transform.position.x, transform.position.y+5, transform.position.z), Quaternion.identity);
   

        // boule.transform.SetParent(this.transform);
        // boule.transform.rotation = Quaternion.identity;
        
        //*******************************************************
        ThrowBallAtTargetLocation(target.position, 50f);


        // yield return new WaitForSeconds(1f);
        // rb = boule.GetComponent<Rigidbody>();
        // rb.AddForce(0,-0.2f,1f, ForceMode.Impulse);
        // rb.AddForce(transform.up * 400f);
        // rb.AddForce(transform.forward * 30f, ForceMode.Impulse);
        tourner = true;
        animator.SetBool("isAttacking", false);
        // boule.transform.SetParent(null);
        StartCoroutine(bouger());
        


       
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
         
           if(delai>400){
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
