using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnnemiEtatsManagerRouge : MonoBehaviour
{
  
    private EnnemiEtatsBaseRouge etatActuel;
    private int nbVies =3;
    [SerializeField] AudioClip ennemiMeurt;
    public EnnemiEtatReposRouge repos = new EnnemiEtatReposRouge();
    public EnnemiEtatPromenadeRouge promenade = new EnnemiEtatPromenadeRouge();
    public EnnemiEtatChasseRouge chasse = new EnnemiEtatChasseRouge();

    public GameObject cible {get;set;}
    public GameObject cibleFee {get;set;}
    public Transform origine {get;set;}

    public NavMeshAgent agent{ get; set;}
    public Animator animator{get; set;}

 

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        ChangerEtat(repos);
        
    }

    void OnTriggerEnter(Collider other)
    {
     
        if(other.tag == "attaque"){
            if(nbVies > 0){
            SoundManager.instance.JouerSon(ennemiMeurt);
            nbVies--;
            }
            if(nbVies==0){
            Destroy(gameObject);
            }
        }

        if(other.tag == "Player"){
            Debug.Log("le joueur perd une vie a cause de l'ennemi rouge");
 
        }
    }

    public void ChangerEtat(EnnemiEtatsBaseRouge etat)
    {
        
        etatActuel = etat;
        etatActuel.InitEtat(this);
    }
    
    // Update is called once per frame
   
}
