using UnityEngine;

public class BiomesEtatCultivable : BiomesEtatsBase
{
  

  public override void InitEtat(BiomesEtatsManager biome)
  {
    
  }
  public override void UpdateEtat(BiomesEtatsManager biome)
  {
    
  }
  public override void TriggerEnterEtat(BiomesEtatsManager biome, Collider other)
  {
    Debug.Log(biome.biomeItem.name);
    //  if(biome.biomeItem != null && biome.biomeItem.name == "i_2_1(Clone)"){
    //   biome.biomeItem.GetComponent<Transform>().localScale = new Vector3(0f,0f,0f);
    // }
      if(biome.biomeItem != null && biome.biomeItem.name == "i_2_1(Clone)" ){
      GameObject.Destroy(biome.biomeItem);
    }

    

  }
}
