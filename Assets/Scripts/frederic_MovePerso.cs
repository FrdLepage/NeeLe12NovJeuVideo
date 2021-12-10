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
    public bool feeExiste =  false;
    private bool peutPerdrevie = true;

    [SerializeField] public GameObject fee;
    private AudioSource _audio;
    private GameObject attaque;
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
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Perd une vie");
       if (other.gameObject.tag=="Lave")
       {
           Debug.Log("Perd une vie sur la lave");
       }
    }

    private void OnParticleCollision(){
        //******mettre ligne pour perdre la vie
        StartCoroutine(PerdreVie());
        Debug.Log("collision perso et particules");
    }

    private IEnumerator PerdreVie(){
        if(peutPerdrevie == true){
            this.GetComponent<HeartSystem>().TakeDamage(1);
            peutPerdrevie = false;
        }
        yield return new WaitForSeconds(4f);
        peutPerdrevie = true;
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && peutAttaquer == true){
            peutAttaquer = false;
            animator.SetTrigger("attaque");
            StartCoroutine(Fee());
        }
        //le joueur quand il clic sur les flèches à gauche ou à droite le joueur se tourne soit vers la gauche ou la droite
        transform.Rotate(0,vitesseRotation*Input.GetAxis("Horizontal"),0);

        //la vitesse s'active par la multiplation puisque Input.GetAxis("Vertical") est à 0 et quand le joueur clic sur les flèche du haut Input.GetAxis("Vertical") se dirige vers le 1
        float vitesse=vitesseMouvement*Input.GetAxis("Vertical"); 

       
        if(!Input.GetButton("Vertical") || !_audio.isPlaying){
            _audio.Play();
        }

        

        //quand le joueur clic sur les flèches du haut l'animation course démarre
        //animator.SetBool("enCourse",vitesse>0);

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

    private IEnumerator SonMarche(float vitesse){
        while(vitesse > 0 ){
        SoundManager.instance.JouerSon(persoMarche);
        }
        yield return null;
    }

    
    private IEnumerator Fee (){
        feeExiste = true;
        yield return new WaitForSeconds(1.18f);
        GameObject uneFee = Instantiate(fee,transform.position, Quaternion.identity);
        // unAgent.GetComponent<EnnemiEtatsManagerRouge>().cibleFee = perso.GetComponent<frederic_MovePerso>().fee;
        SoundManager.instance.JouerSon(persoAttaque);
        var particules = Resources.Load("magic_circle") as GameObject;
        GameObject system = Instantiate(particules, transform.position, Quaternion.identity);
        // var particules = Resources.Load("splash") as GameObject;
        // GameObject system = Instantiate(particules, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(5f);

        peutAttaquer = true;
        yield return new WaitForSeconds(5f);
        Destroy(uneFee);
       
        // DestroyImmediate(fee, true);
        feeExiste = false;
        yield return null;
    }


    

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
         if(other.tag == "EnnemiRouge" && peutPerdrevie == true){
           StartCoroutine(PerdreVie());
            Debug.Log("le joueur perd une vie a cause de l'ennemi rouge");
 
        }
        if(other.tag == "Potion"){
            Debug.Log("contact avec potion");
            Destroy(other.gameObject);
            StartCoroutine(Sphere());
        }

        
        else if(other.tag == "Lave")
        {

            SceneManager.LoadScene("Perdu");
        }
    }


    private IEnumerator Sphere (){
        peutPerdrevie = false;
        attaque = Resources.Load("protect") as GameObject;
        GameObject Sphere = Instantiate(attaque,transform.position, Quaternion.identity);
        Sphere.GetComponent<Transform>().localScale = new Vector3(0,0,0);
        Sphere.GetComponent<Transform>().position = new Vector3(transform.position.x, transform.position.y+2.2f, transform.position.z);
        Sphere.GetComponent<Transform>().parent = transform;
        
      
        float scale = 0;

        while(scale <= 40){
            scale+=Time.deltaTime+5;
          
            // attaque.GetComponent<Transform>().localScale = Vector3.Lerp(new Vector3(0,0,0),new Vector3(20,20,20), scale);
            Sphere.GetComponent<Transform>().localScale = new Vector3(scale, scale, scale); 
            yield return null;
        }
        SoundManager.instance.JouerSon(persoBulle);
        yield return new WaitForSeconds(10f);
        while(scale >= 40){
            scale -=Time.deltaTime+10;
            Sphere.GetComponent<Transform>().localScale = new Vector3(scale, scale, scale);
            yield return null;
        }
        Destroy(Sphere);
        peutPerdrevie = true;
        yield return null;
    }
}
