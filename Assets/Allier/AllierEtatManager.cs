using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AllierEtatManager : MonoBehaviour
{

    private AllierEtatBase etatActuel;
    public AllierEtatRepos repos = new AllierEtatRepos();
    public AllierEtatChasse chasse = new AllierEtatChasse();

    public GameObject cible {get;set;}
    public Transform origine {get;set;}

    public NavMeshAgent agent{ get; set;}

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        ChangerEtat(repos);
    }

    public void ChangerEtat(AllierEtatBase etat)
    {
        
        etatActuel = etat;
        etatActuel.InitEtat(this);
    }

}
