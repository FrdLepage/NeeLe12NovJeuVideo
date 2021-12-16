using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class frederic_MovePerso : MonoBehaviour
{
    [SerializeField] AudioClip persoAttaque;
    [SerializeField] AudioClip persoMarche;
    [SerializeField] AudioClip persoSaut;
    [SerializeField] AudioClip persoAtteri;
    [SerializeField] AudioClip persoBulle;
    [SerializeField] private float vitesseMouvement=20.0f;
    [SerializeField] private float vitesseRotation=20.0f;
    [SerializeField] private float impulsionSaut=20.0f;
    [SerializeField] private float gravite=20.0f;
    [SerializeField] private Camera cameraDuJoueur;

    [SerializeField] private GameObject ChampDeForce;

    private float vitesseSaut;

    private bool peutPerdrevie = true;

    [SerializeField] public GameObject fee;
    private AudioSource _audio;
    private GameObject attaque;
    private Vector3 directionsMouvement= Vector3.zero;
    private bool peutAttaquer = true;
    Animator animator;
    CharacterController controller;

    void Awake()
    {
        animator=GetComponent<Animator>(); //association du composant Animator
        controller=GetComponent<CharacterController>(); //association du composant CharacterController
        _audio = GetComponent<AudioSource>(); //association du composant AudioSource
    }

    private void OnParticleCollision(){
        StartCoroutine(PerdreVie());
    }

    /// <summary>
    /// Coroutine qui s'occupe de faire perdre de la vie au personnage
    /// </summary>
    private IEnumerator PerdreVie(){
        //si perdre vie est vrai
        if(peutPerdrevie == true){
            this.GetComponent<HeartSystem>().TakeDamage(1); //appel de la fonction TakeDamage pour retirer une vie
            peutPerdrevie = false; //le perso ne peut plus perdre de vie pendant un certain moment
        }
        yield return new WaitForSeconds(4f); //delai de 4 secondes
        peutPerdrevie = true; // le perso peut a nouveau perdre une vie
        yield return null;
    }

    void Update()
    {
        //si le joueur appui sur la touche E et que peutAttaquer est vrai
        if(Input.GetKeyDown(KeyCode.E) && peutAttaquer == true){
            peutAttaquer = false; //peutAttaquer devient faux
            animator.SetTrigger("attaque"); //l'animation de l'attaque est declenchee
            StartCoroutine(Fee()); //debut de la coroutine pour generer la fee
        }
        //le joueur quand il clic sur les flèches à gauche ou à droite le joueur se tourne soit vers la gauche ou la droite
        transform.Rotate(0,vitesseRotation*Input.GetAxis("Horizontal"),0);
        //la vitesse s'active par la multiplation puisque Input.GetAxis("Vertical") est à 0 et quand le joueur clic sur les flèche du haut Input.GetAxis("Vertical") se dirige vers le 1
        float vitesse=vitesseMouvement*Input.GetAxis("Vertical"); 

       //pour declencher les bruits de pas
        if(!Input.GetButton("Vertical") || !_audio.isPlaying){
            _audio.Play();
        }

        animator.SetFloat("vitesseX", vitesse); // association de la valeur de vitesse au float vitesseX pour le blende tree
        animator.SetFloat("vitesseY", Input.GetAxis("Jump")); // association de la valeur de l'input Jump au float vitesseY pour le blende tree

        //le personnage avance ou recule selon la vitesse du float à la ligne 31
        directionsMouvement=new Vector3(0,0,vitesse);

         //on transforme son mouvement en world space
        directionsMouvement= transform.TransformDirection(directionsMouvement);

        //si le joueur clic sur le bouton jump et il est au sol
        if ( Input.GetButton("Jump")&&controller.isGrounded) {
            vitesseSaut=impulsionSaut;
            SoundManager.instance.JouerSon(persoSaut); //son pour le saut du perso
            }

            //fait en sorte que le personnage saute dans les airs avec la variable impulsionSaut
            directionsMouvement.y +=vitesseSaut;
        //quand le joueur ne touche pas au saut fait descendre le personnage par la variable de gravite
        if(!controller.isGrounded)  vitesseSaut-=gravite;

        //bouge le personnage selon le mouvement qu'on lui donne et multiplie-le au temps 
        controller.Move(directionsMouvement*Time.deltaTime);

        //si le joueur se promène augmente de scale de la sphère
        ChampDeForce.transform.localScale=Vector3.one*vitesse*2;

       //ajustement du field of view pour eviter que les arbres cache la camera
        cameraDuJoueur.GetComponent<Camera>().fieldOfView = vitesse+60;

    }

    /// <summary>
    /// Coroutine responsable de jouer le son marche
    /// </summary>
    /// <param name="vitesse"></param>
    private IEnumerator SonMarche(float vitesse){
        //tant que la vitesse est plus grande que 0
        while(vitesse > 0 ){
        SoundManager.instance.JouerSon(persoMarche);
        }
        yield return null;
    }

    
   
    private IEnumerator Fee (){
  
        vitesseMouvement=0f;//rendre le personnage immobile
        // unAgent.GetComponent<EnnemiEtatsManagerRouge>().cibleFee = perso.GetComponent<frederic_MovePerso>().fee;
        yield return new WaitForSeconds(1f);

        var particules = Resources.Load("magic_circle") as GameObject;//load les particule du jeu
        GameObject system = Instantiate(particules, transform.position, Quaternion.identity);//instacie les particule a l'endroit
        system.transform.Rotate(-90,0,0);//rotate le system de particule de 90 degré

        var particules2 = Resources.Load("splash") as GameObject;//load les particule du jeu
        GameObject system2 = Instantiate(particules2, transform.position, Quaternion.identity);////instacie les particule a l'endroit
        system2.transform.Rotate(-90,0,0);//rotate le system de particule de 90 degré
        

        // yield return new WaitForSeconds(2f);
        SoundManager.instance.JouerSon(persoAttaque);//jouer le son d'attaquer

        yield return new WaitForSeconds(1.1f);
        vitesseMouvement=20f;//reprendre les mouvement du personnage
        GameObject uneFee = Instantiate(fee,new Vector3(transform.position.x,transform.position.y+2,transform.position.z), Quaternion.identity);//instancie la fee

        yield return new WaitForSeconds(10f);//Maintenir la fee vivant pendant 10 seconde

        Destroy(uneFee);//Detruire la fee
        peutAttaquer = true;//peut réattaquer
       
        // DestroyImmediate(fee, true);
        yield return null;
    }


    
    /// <summary>
    /// Detecte les collisions avec les autres collider
    /// </summary>
    /// <param name="other">Collider</param>
    void OnTriggerEnter(Collider other)
    {
        //si le perso entre en collision avec l'Ennemi Rouge
         if(other.tag == "EnnemiRouge" && peutPerdrevie == true){
           StartCoroutine(PerdreVie()); //appel de la coroutine pour perdre de la vie
        }
        //si le perso entre en collision avec la potion
        if(other.tag == "Potion"){
            Destroy(other.gameObject); //destruction de la potion
            StartCoroutine(Sphere()); //appel de la coroutine pour generer la sphere
        }
        //si le personnage entre en collision avec la lave qui possede la tag Lave
        else if(other.tag == "Lave")
        {
            SceneManager.LoadScene("Perdu"); //generer la scene de defaite
        }
    }


    /// <summary>
    /// Permet de generer la bulle de protection autour du personnage
    /// </summary>
    private IEnumerator Sphere (){
        peutPerdrevie = false; //enleve la possibilite de perdre des vies
        attaque = Resources.Load("protect") as GameObject; //pour aller chercher la bulle dans le dossier ressource
        GameObject Sphere = Instantiate(attaque,transform.position, Quaternion.identity); // la sphere est instanciee
        Sphere.GetComponent<Transform>().localScale = new Vector3(0,0,0); //la taille de la sphere est de 0
        //la sphere est generee a la position du personnage
        Sphere.GetComponent<Transform>().position = new Vector3(transform.position.x, transform.position.y+2.2f, transform.position.z);
        Sphere.GetComponent<Transform>().parent = transform; //la sphere est associee comme enfant du personnage
        float scale = 0; // variable de la taille de la bulle
        //tant que la taille est plus petite ou egale a 40
        while(scale <= 40){
            //la taille  augmente selon le temps
            scale+=Time.deltaTime+5;
            //la taille de la bulle est ajustee selon la variable scale
            Sphere.GetComponent<Transform>().localScale = new Vector3(scale, scale, scale); 
            yield return null;
        }
        SoundManager.instance.JouerSon(persoBulle);//un son est joue
        yield return new WaitForSeconds(10f); //la bulle reste 10 secondes
        //tant que la taille est pus grande ou egale a 40
        while(scale >= 40){
            scale -=Time.deltaTime+5; //la taille de la variable scale diminue selon le temps
            //la taille de la bulle est ajustee selon la variable scale
            Sphere.GetComponent<Transform>().localScale = new Vector3(scale, scale, scale);
            yield return null;
        }
        Destroy(Sphere); //la sphere est detruite
        peutPerdrevie = true; //la possibilite de perdre des vies redevient vraie
        yield return null;
    }
}
