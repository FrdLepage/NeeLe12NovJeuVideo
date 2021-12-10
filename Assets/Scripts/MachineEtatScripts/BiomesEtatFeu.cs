using System.Collections;
using UnityEngine;


public class BiomesEtatFeu : BiomesEtatsBase
{

  
  public override void InitEtat(BiomesEtatsManager biome)
  {
      biome.StartCoroutine(Boule(biome));    
  }
  public override void UpdateEtat(BiomesEtatsManager biome)
  {
    
  }
     private IEnumerator Boule(BiomesEtatsManager biome){
        var particules = Resources.Load("particules") as GameObject;
        GameObject.Instantiate(particules, biome.transform.position, Quaternion.identity);
        biome.ChangerEtat(biome.activable);
        yield return null;
    }
  public override void TriggerEnterEtat(BiomesEtatsManager biome, Collider other)
  {
          

  }
}
