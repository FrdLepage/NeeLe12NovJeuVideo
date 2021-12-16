using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnnemiEtatsManagerRouge : MonoBehaviour
{
  
    private EnnemiEtatsBaseRouge etatActuel; //l'etat actuel de l'ennemis
    public EnnemiEtatReposRouge repos = new EnnemiEtatReposRouge();//Etat Repos
    public EnnemiEtatPromenadeRouge promenade = new EnnemiEtatPromenadeRouge();//Etat Promenade
    public EnnemiEtatChasseRouge chasse = new EnnemiEtatChasseRouge();//Etat Chasse
    public GameObject cible {get;set;}//le personnage de cible
    public Transform origine {get;set;}//Position origin
    public NavMeshAgent agent{ get; set;}//Agent du navMesh
    public Animator animator{get; set;}//l'animator de l'ennemis

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();//get agent
        animator = GetComponent<Animator>();//get le animator
        ChangerEtat(repos);//changer l'etat de l'ennemis a repos
        
    }

    void OnTriggerEnter(Collider other)
    {   
        etatActuel.TriggerEnterEtat(this,other);
    }

    public void ChangerEtat(EnnemiEtatsBaseRouge etat)
    {
        etatActuel = etat;
        etatActuel.InitEtat(this);
    }
    
    // Update is called once per frame
   
}
