using System.Collections;
using UnityEngine;


public class BiomesEtatFeu : BiomesEtatsBase
{
  public override void InitEtat(BiomesEtatsManager biome)
  {
      biome.StartCoroutine(Boule(biome)); //appel de la coroutine s'occupe de produire l'effet de feu
  }
  
  /// <summary>
  /// Permet de déclencher l'effet de feu sur les biomes
  /// et donc d'instancier le système de particules.
  /// </summary>
  /// <param name="biome">biome d type BiomesEtatsManager</param>
  private IEnumerator Boule(BiomesEtatsManager biome){
    //Aller chercher le systeme de particules dans le dossier resources
    GameObject particules = Resources.Load("particules") as GameObject;
    //Instanciation du systeme de particules a la position du biome
    GameObject.Instantiate(particules, biome.transform.position, Quaternion.identity);
    //l'etat du biome est changer vers l'etat activable
    biome.ChangerEtat(biome.activable);
    yield return null;
  }
  public override void TriggerEnterEtat(BiomesEtatsManager biome, Collider other)
  {
          

  }
}
