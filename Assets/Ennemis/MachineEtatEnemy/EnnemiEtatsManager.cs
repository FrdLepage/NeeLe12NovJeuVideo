using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnnemiEtatsManager : MonoBehaviour
{
  
    private EnnemiEtatsBase etatActuel;
    [SerializeField] AudioClip ennemiMeurt;
    private int nbVies = 3;
    public EnnemiEtatRepos repos = new EnnemiEtatRepos();
    public EnnemiEtatPromenade promenade = new EnnemiEtatPromenade();
    public EnnemiEtatChasse chasse = new EnnemiEtatChasse();

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
            if(nbVies > 0){
            SoundManager.instance.JouerSon(ennemiMeurt);
            nbVies--;
            }
            if(nbVies==0){
            Destroy(gameObject);
            }
            
        }
    }

    public void ChangerEtat(EnnemiEtatsBase etat)
    {
        
        etatActuel = etat;
        etatActuel.InitEtat(this);
    }
    
    // Update is called once per frame
   
}
