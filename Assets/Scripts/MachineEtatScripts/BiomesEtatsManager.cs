using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomesEtatsManager : MonoBehaviour
{
    [SerializeField] public AudioClip collecterCristal;
    [SerializeField] public Material matParticules;
    private BiomesEtatsBase etatActuel;
    public BiomesEtatActivable activable = new BiomesEtatActivable();
    public BiomesEtatCultivable cultivable = new BiomesEtatCultivable();
    public BiomesEtatFeu feu = new BiomesEtatFeu();
    
    public GameObject point{ get; set;}

    public Material biomeMateriel{ get; set;}

    public GameObject biomeItem {get;set;}

    // Start is called before the first frame update
    void Start()
    {
        ChangerEtat(activable);
        point.GetComponent<SystemeDePoint>().SetMaxPoint(200);
    }

    public void ChangerEtat(BiomesEtatsBase etat)
    {
        
        etatActuel = etat;
        etatActuel.InitEtat(this);
    }
    
    // Update is called once per frame
    // void Update()
    // {
    //     etatActuel.UpdateEtat(this);
    // }
  
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(this);
        Debug.Log(other);
        etatActuel.TriggerEnterEtat(this,other);

    }
}
