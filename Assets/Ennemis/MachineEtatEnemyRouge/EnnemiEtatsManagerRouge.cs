using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnnemiEtatsManagerRouge : MonoBehaviour
{
  
    private EnnemiEtatsBaseRouge etatActuel;
    [SerializeField] AudioClip ennemiMeurt;
    public EnnemiEtatReposRouge repos = new EnnemiEtatReposRouge();
    public EnnemiEtatPromenadeRouge promenade = new EnnemiEtatPromenadeRouge();
    public EnnemiEtatChasseRouge chasse = new EnnemiEtatChasseRouge();

    public GameObject cible {get;set;}
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
            SoundManager.instance.JouerSon(ennemiMeurt);
            Destroy(gameObject);
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
