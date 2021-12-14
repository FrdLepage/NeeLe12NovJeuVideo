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
        float firingElevationAngle = FiringElevationAngle(Physics.gravity.magnitude, distance, initialVelocity);
        Vector3 elevation = Quaternion.AngleAxis(firingElevationAngle, transform.right) * transform.up;
        float directionAngle = AngleBetweenAboutAxis(transform.forward, direction, transform.up);
        Vector3 velocity = Quaternion.AngleAxis(directionAngle, transform.up) * elevation * initialVelocity;
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
        float angle1 = Mathf.Asin((180f));
        float angle2 = 0.5f * Mathf.Asin((gravity * distance) / (initialVelocity * initialVelocity));
        float angle = 0.5f * Mathf.Asin((gravity * distance) / (initialVelocity * initialVelocity)) * Mathf.Rad2Deg;
        return angle;
    }


    private IEnumerator DeclencherAttaque(){

        //animation
        animator.SetBool("isAttacking", true);
        yield return new WaitForSeconds(3f);
        //instantier boule
        boule = GameObject.Instantiate((GameObject)Resources.Load("BouleFeu"), new Vector3(transform.position.x, transform.position.y+5, transform.position.z), Quaternion.identity);
        ThrowBallAtTargetLocation(target.position, 20f);
        tourner = true;
        animator.SetBool("isAttacking", false);
        StartCoroutine(bouger());
        yield return new WaitForSeconds(1f);
      


    }

    private IEnumerator bouger(){

    float startRotation = transform.eulerAngles.y;
    float endRotation = startRotation + 360.0f;
    float delai = 0f;

     while ( tourner )
     {
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

   //source: How to throw a ball to a specific point on plane? https://stackoverflow.com/questions/30290262/how-to-throw-a-ball-to-a-specific-point-on-plane
}
