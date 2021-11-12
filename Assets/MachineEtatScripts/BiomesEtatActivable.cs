using System.Collections;
using UnityEngine;

public class BiomesEtatActivable : BiomesEtatsBase
{
  
  float rotationT = 0.0f;
  float duration = 0.3f;
 
  
  public override void InitEtat(BiomesEtatsManager biome)
  {
      // Debug.Log("allo activable");
  }
  public override void UpdateEtat(BiomesEtatsManager biome)
  {

  }
  public override void TriggerEnterEtat(BiomesEtatsManager biome, Collider other)
  {
    if(other.tag == "Player"){
      biome.StartCoroutine(bob(biome));
    }

  }

  private IEnumerator bob(BiomesEtatsManager biome){
    
    
     
      // var em = ps.emission;
      

      // while(particule < durationParticule){
      //   particule+=Time.deltaTime;
      //   em.enabled = true;
      //   yield return null;
      // }

      // em.enabled = false;


  //   while(sizeT<duration)
  //   {
  //   sizeT+=Time.deltaTime;
  //   biome.GetComponent<BoxCollider>().size = new Vector3(2,2,2);
  //   biome.GetComponent<Transform>().localScale = new Vector3(0.5f,0.5f,0.5f);
  //   biome.GetComponent<Renderer>().material = Resources.Load("b5_1") as Material;
  // //  ICI IL FAUT CHANGER LE GLOW IL EST BEN TROP MAUVE 

  //  // Multiplie de 1000 le size Utilisation du while dans le projet
    
  //   yield return null;
  //   }

    biome.GetComponent<Transform>().localScale = new Vector3(1f,1f,1f);
    biome.GetComponent<BoxCollider>().size = new Vector3(1f,1f,1f);
    biome.GetComponent<Renderer>().material = biome.biomeMateriel;
    

    // yield return new WaitForSeconds(0.2f);

    
    while(rotationT<duration){
      rotationT+=Time.deltaTime;

      // ParticleSystem ps = biome.AddComponent<ParticleSystem>();
      var p = biome.gameObject.AddComponent<ParticleSystem>();
   
      // var Main = p.main;
      // Main.loop=false;
      // Main.duration=1f;

      
 
      biome.GetComponent<ParticleSystemRenderer>().material = biome.matParticules;
      // ParticleSystem p = biome.GetComponent<ParticleSystem>();
      // biome.GetComponent<Transform>().localRotation= Quaternion.Euler(90,90,90);
      // gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_YourParameter", someValue);
    //  biome.GetComponent<Renderer>().sharedMaterial.SetColor("_EmissionColor", )
      biome.GetComponent<Renderer>().material = Resources.Load("materiaux/main_cristal") as Material;
    //  biome.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
    //  biome.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(10f,10f,10f));
    //  biome.GetComponent<Renderer>().material.SetColor("_Color", new Color(255f,255f,0f));

  

      var transform = biome.GetComponent<Transform>();
      // transform.Rotate(Vector3.forward,5);
      //tou complet
      yield return null;
    
    }
    
    // var main = ps.main;

    
    // main.stopAction = ParticleSystemStopAction.Destroy;

    biome.GetComponent<Renderer>().material = biome.biomeMateriel;
    // Multiplie de 1000 le size Utilisation du while dans le projet
    
  
    // biome.GetComponent<Transform>().localRotation= Quaternion.Euler(0,0,0);

    yield return new WaitForSeconds(0.5f);
    ParticleSystem ps = biome.GetComponent<ParticleSystem>();
    ps.Clear();
    var em = ps.emission;
    em.enabled = false;

    Vector3 position = new Vector3(0,0,0);
    position = biome.transform.position;
    if(biome.biomeItem != null){
      biome.biomeItem.GetComponent<Transform>().localScale = new Vector3(0.05f,0.05f,0.05f);

    }
    // GameObject champignons = GameObject.Instantiate(Resources.Load("items/champignons"), position, Quaternion.identity) as GameObject;
    biome.ChangerEtat(biome.cultivable);
    
    yield return null;
  }
  
}
