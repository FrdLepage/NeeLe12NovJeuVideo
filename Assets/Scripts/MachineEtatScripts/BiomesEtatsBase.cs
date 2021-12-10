using UnityEngine;

public abstract class BiomesEtatsBase
{
  public abstract void InitEtat(BiomesEtatsManager biome);
  // public abstract void UpdateEtat(BiomesEtatsManager biome);
  public abstract void TriggerEnterEtat(BiomesEtatsManager biome, Collider other);

}
