using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frederic_MovePerso : MonoBehaviour
{
    [SerializeField] AudioClip persoAttaque;
    [SerializeField] AudioClip persoMarche;
    [SerializeField] AudioClip persoSaut;
    [SerializeField] AudioClip persoAtteri;
    [SerializeField] private float vitesseMouvement=20.0f;
    [SerializeField] private float vitesseRotation=20.0f;
    [SerializeField] private float impulsionSaut=20.0f;
    [SerializeField] private float gravite=20.0f;
    // [SerializeField] private GameObject attaque;
    [SerializeField] private Camera cameraDuJoueur;

    [SerializeField] private GameObject ChampDeForce;

    private float vitesseSaut;
    

    private GameObject attaque;
    private AudioSource _audio;
    private Vector3 directionsMouvement= Vector3.zero;
    private bool peutAttaquer = true;

    Animator animator;
    CharacterController controller;
    // Start is called before the first frame update
    void Awake()
    {
        animator=GetComponent<Animator>();
        controller=GetComponent<CharacterController>();
        _audio = GetComponent<AudioSource>();
       
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && peutAttaquer == true){
            peutAttaquer = false;
            animator.SetTrigger("attaque");
            StartCoroutine(Sphere());
        }
//le joueur quand il clic sur les flèches à gauche ou à droite le joueur se tourne soit vers la gauche ou la droite
        transform.Rotate(0,vitesseRotation*Input.GetAxis("Horizontal"),0);

        //la vitesse s'active par la multiplation puisque Input.GetAxis("Vertical") est à 0 et quand le joueur clic sur les flèche du haut Input.GetAxis("Vertical") se dirige vers le 1
        float vitesse=vitesseMouvement*Input.GetAxis("Vertical"); 

       
        if(!Input.GetButton("Vertical") || !_audio.isPlaying){
            _audio.Play();
        }

        

        //quand le joueur clic sur les flèches du haut l'animation course démarre
        // animator.SetBool("enCourse",vitesse>0);

        animator.SetFloat("vitesseX", vitesse);
        
        
        animator.SetFloat("vitesseY", Input.GetAxis("Jump"));

        //le personnage avance ou recule selon la vitesse du float à la ligne 31
        directionsMouvement=new Vector3(0,0,vitesse);

         //on transforme son mouvement en world space
        directionsMouvement= transform.TransformDirection(directionsMouvement);

        //si le joueur clic sur le bouton jump et il est au sol
        if ( Input.GetButton("Jump")&&controller.isGrounded) {
            vitesseSaut=impulsionSaut;
            SoundManager.instance.JouerSon(persoSaut);
            }
            //active l'animation du saut si le joueur n'est pas par terre et si la vitesse du saut est plus grande
            // animator.SetBool("enSaut",!controller.isGrounded&&vitesseSaut>-impulsionSaut);

            //fait en sorte que le personnage saute dans les airs avec la variable impulsionSaut
            directionsMouvement.y +=vitesseSaut;
        //quand le joueur ne touche pas au saut fait descendre le personnage par la variable de gravite
        if(!controller.isGrounded)  vitesseSaut-=gravite;

        //bouge le personnage selon le mouvement qu'on lui donne et multiplie-le au temps 
        controller.Move(directionsMouvement*Time.deltaTime);

        //si le joueur se promène augmente de scale de la sphère
        ChampDeForce.transform.localScale=Vector3.one*vitesse*2;


        cameraDuJoueur.GetComponent<Camera>().fieldOfView = vitesse+60;

    }

    
    void Attaque(){
    Instantiate(attaque,transform.position, Quaternion.identity);
    }

    private IEnumerator SonMarche(float vitesse){
        while(vitesse > 0 ){
        SoundManager.instance.JouerSon(persoMarche);

        }
        yield return null;
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //      if(other.gameObject.CompareTag("Lave")){
    //         transform.position=new Vector3(107.5f,41.7f,99.5f);
    //     }
    // }
    private IEnumerator Sphere (){
        SoundManager.instance.JouerSon(persoAttaque);
        yield return new WaitForSeconds(1f);
        attaque = Resources.Load("Sphere") as GameObject;
        var Sphere = Instantiate(attaque,transform.position, Quaternion.identity);
        Sphere.GetComponent<Transform>().localScale = new Vector3(0,0,0);
      
        float scale = 0;

        while(scale <= 20){
            scale+=Time.deltaTime+3;
          
            // attaque.GetComponent<Transform>().localScale = Vector3.Lerp(new Vector3(0,0,0),new Vector3(20,20,20), scale);
            Sphere.GetComponent<Transform>().localScale = new Vector3(scale, scale/1.5f, scale);

            yield return null;
        }
        yield return new WaitForSeconds(0.2f);
        Destroy(Sphere);

        peutAttaquer = true;
        yield return null;
    }
}
