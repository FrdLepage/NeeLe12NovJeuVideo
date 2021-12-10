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
    
    public GameObject point{ get; set;}

    public Material biomeMateriel{ get; set;}

    public GameObject biomeItem {get;set;}

    // Start is called before the first frame update
    void Start()
    {
        point.GetComponent<SystemeDePoint>().SetMaxPoint(200);
        ChangerEtat(activable);
    }

    public void ChangerEtat(BiomesEtatsBase etat)
    {
        
        etatActuel = etat;
        etatActuel.InitEtat(this);
    }
    
    // Update is called once per frame
    void Update()
    {
        etatActuel.UpdateEtat(this);
      
 
    
    }
  
    
    private void OnTriggerEnter(Collider other)
    {
           if(other.tag == "Boule"){
            
               StartCoroutine(Boule());
             
        }
        
        etatActuel.TriggerEnterEtat(this,other);
    }

   
    private IEnumerator Boule(){
        // var p = gameObject.AddComponent<ParticleSystem>();
        var particules = Resources.Load("particules") as GameObject;
        Instantiate(particules, transform.position, Quaternion.identity);

        // GetComponent<Renderer>().material = Resources.Load("materiaux/main_cristal") as Material;

        // yield return new WaitForSeconds(10f);

        // ParticleSystem ps = GetComponent<ParticleSystem>();
        // ps.Clear();
        // var em = ps.emission;
        // em.enabled = false;

        yield return null;
    }


}
