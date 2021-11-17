using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcEtatsManager : MonoBehaviour
{
  
    private NpcEtatsBase etatActuel;
    public NpcEtatRepos repos = new NpcEtatRepos();
    public NpcEtatPromenade promenade = new NpcEtatPromenade();
    public NpcEtatChasse chasse = new NpcEtatChasse();

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

    public void ChangerEtat(NpcEtatsBase etat)
    {
        
        etatActuel = etat;
        etatActuel.InitEtat(this);
    }
    
    // Update is called once per frame
   
}
