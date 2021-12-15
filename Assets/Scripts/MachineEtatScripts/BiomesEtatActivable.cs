using System.Collections;
using UnityEngine;

public class BiomesEtatActivable : BiomesEtatsBase
{
  public GameObject point{ get; set;}
  float delai = 0.0f;
  float duration = 0.3f;
 
  
  public override void InitEtat(BiomesEtatsManager biome)
  {
      // Debug.Log("allo activable");
  }
  // public override void UpdateEtat(BiomesEtatsManager biome)
  // {
 

  // }
  public override void TriggerEnterEtat(BiomesEtatsManager biome, Collider other)
  {
    if(other.tag == "Player"){
      biome.StartCoroutine(bob(biome));
    }

    //Quand le boule de feu entre en collision avec un biome
           if(other.tag == "Boule"){
               //changement d'état vers etat feu
               biome.ChangerEtat(biome.feu);            
        }


  }



     private IEnumerator RetransformerBiomes(){
        yield return null;
    }



  private IEnumerator bob(BiomesEtatsManager biome){

    biome.GetComponent<Transform>().localScale = new Vector3(1f,1f,1f);
    // biome.GetComponent<BoxCollider>().size = new Vector3(1f,1f,1f);
    biome.GetComponent<Renderer>().material = biome.biomeMateriel;
    
    //tant que le delai est plus petite que la duration
    while(delai<duration){
      delai+=Time.deltaTime;//le delai augmente selon le temps
      //si il n'y a pas deja de systeme de particules sur le biome
      if(!biome.gameObject.GetComponent<ParticleSystem>()){
        //ajout d'un systeme de particules sur le biome
        Component p = biome.gameObject.AddComponent<ParticleSystem>();
      }
      //association du materiel des particules
      biome.GetComponent<ParticleSystemRenderer>().material = biome.matParticules;
      //pour aller chercher le materiel mauve dans les ressources et l'appliquer sur le biome
      biome.GetComponent<Renderer>().material = Resources.Load("materiaux/main_cristal") as Material;
      yield return null;
    }
    
    biome.GetComponent<Renderer>().material = biome.biomeMateriel;
    yield return new WaitForSeconds(0.5f);
    ParticleSystem ps = biome.GetComponent<ParticleSystem>();
    ps.Clear();
    var em = ps.emission;
    em.enabled = false;

    Vector3 position = new Vector3(0,0,0);
    position = biome.transform.position;
    if(biome.biomeItem != null){
      biome.biomeItem.GetComponent<Transform>().localScale = new Vector3(0.1f,0.1f,0.1f);
    }
    biome.ChangerEtat(biome.cultivable);
    yield return null;
  }
  
}
