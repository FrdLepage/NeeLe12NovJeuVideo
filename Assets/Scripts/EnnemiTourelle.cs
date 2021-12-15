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

    void Start()
    {
        animator = GetComponent<Animator>(); //Association du composant Animator
        StartCoroutine(bouger()); //Depart de la coroutine responssable de faire tourner l'ennemi
    }

     /// <summary>
     /// Permet de lancer la balle en visant la cible avec une force initiale en fonction
     /// de la gravite
     /// </summary>
     /// <param name="targetLocation">La position de la cible, vecteur 3</param>
     /// <param name="initialVelocity">La force initiale du lance</param>
    public void ThrowBallAtTargetLocation(Vector3 targetLocation, float initialVelocity)
    {
        //calcul la direction que dois prendre la boule
        Vector3 direction = (targetLocation - transform.position).normalized;
        //calcul de la distance entre l'ennemi et la cible qui est le personnage
        float distance = Vector3.Distance(targetLocation, transform.position);
        //calcul de l'angle avec lequel la boule doit partir
        float firingElevationAngle = FiringElevationAngle(Physics.gravity.magnitude, distance, initialVelocity);
        //calcul de l'elevation que la boule doit avoir
        Vector3 elevation = Quaternion.AngleAxis(firingElevationAngle, transform.right) * transform.up;
        // cacul de l'angle de la boule pour la duree de sont trajet
        float directionAngle = AngleBetweenAboutAxis(transform.forward, direction, transform.up);
        //calcul de la velocite en foction de l'angle de direction, de la force initiale et de l'elevation
        Vector3 velocity = Quaternion.AngleAxis(directionAngle, transform.up) * elevation * initialVelocity;
        //association du composant Rigidbody de la boule
        rb = boule.GetComponent<Rigidbody>();
       //ajout de la force sur la boule
        rb.AddForce(velocity, ForceMode.VelocityChange);
    }

  
    /// <summary>
    /// Permet de calculer l'angle entre deux points en fonction de l'axe de direction
    /// </summary>
    /// <param name="v1">Premier point de type vecteur 3</param>
    /// <param name="v2">Deuxieme point de type vecteur 3</param>
    /// <param name="n">Axe de direction de type vecteur 3</param>
    /// <returns></returns>
    public static float AngleBetweenAboutAxis(Vector3 v1, Vector3 v2, Vector3 n)
    {
        //retourne l'arc tangente
        return Mathf.Atan2(
            // calcul de la magnitude du produit des deux vecteurs
            Vector3.Dot(n, Vector3.Cross(v1, v2)), 
            Vector3.Dot(v1, v2)) * Mathf.Rad2Deg;
    }

    // Helper method to find angle of elevation (ballistic trajectory) required to reach distance with initialVelocity
    // Does not take wind resistance into consideration.
    /// <summary>
    /// Permet de calculer et de retourner l'angle d'elevation pour parcourir la bonne distance
    /// en prenant compte de la gravite
    /// </summary>
    /// <param name="gravity">La gravite en float</param>
    /// <param name="distance">la distance a parcourir en float</param>
    /// <param name="initialVelocity">La force initiale en float</param>
    private float FiringElevationAngle(float gravity, float distance, float initialVelocity)
    {
        float angle = 0.5f * Mathf.Asin((gravity * distance) / (initialVelocity * initialVelocity)) * Mathf.Rad2Deg;
        return angle;
    }

    /// <summary>
    /// Coroutine qui permet de declencher l'attaque de l'ennemi comprenant l'animation et
    /// l'instantiation de la boule de feu
    /// </summary>
    private IEnumerator DeclencherAttaque(){
        animator.SetBool("isAttacking", true);//déclenchement de l'animation d'attaque
        yield return new WaitForSeconds(3f); 
        //instantiation de la boule de feu a la position de l'ennemi a 5 unites en haut du sol en y
        boule = GameObject.Instantiate((GameObject)Resources.Load("BouleFeu"), new Vector3(transform.position.x, transform.position.y+5, transform.position.z), Quaternion.identity);
        ThrowBallAtTargetLocation(target.position, 20f); //appel de la fonction pour que la balle soit lancee
        tourner = true; //devient vrai quand l'ennemi dit tourner sur lui meme
        animator.SetBool("isAttacking", false); //arret de l'animation
        StartCoroutine(bouger()); //declenchement de la coroutine qui permet a l'ennemi de tourner
        yield return new WaitForSeconds(1f);
    }

    /// <summary>
    /// Permet a l'ennemi de tourner sur lui meme, tant que le booleen tourner est vrai
    /// </summary>
    private IEnumerator bouger(){
    float startRotation = transform.eulerAngles.y; //position de depart pour la rotation
    float endRotation = startRotation + 360.0f; //la rotation de fin correspond a la rotation de depart plus 360 degres 
    float delai = 0f; //declaration de la variable delai
    //l'ennemi tourne tant que le booleen tourner est vrai
     while ( tourner )
     {
         //La rotation s'effectue
         transform.Rotate(new Vector3(0, -1, 0), 20f * Time.deltaTime);
         //la valeur du delai augmente
         delai++;
         //quand le delai atteint 400
           if(delai>400){
            tourner = false; //ennemi arrete de tourner
            delai = 0; //le delai repart a 0
            StartCoroutine(DeclencherAttaque()); //la coroutine pour l'attaque est declenchee
        }
         yield return null;
     }
  }
   //source: How to throw a ball to a specific point on plane? https://stackoverflow.com/questions/30290262/how-to-throw-a-ball-to-a-specific-point-on-plane
}
