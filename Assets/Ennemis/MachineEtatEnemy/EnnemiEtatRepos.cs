 using System.Collections;
using UnityEngine;

public class EnnemiEtatRepos : EnnemiEtatsBase
{ 
  
  public override void InitEtat(EnnemiEtatsManager ennemi)
  {
    //ennemi. pour avoir la reference au monobehavior
      ennemi.StartCoroutine(anime(ennemi));
  }
  

  private IEnumerator anime(EnnemiEtatsManager ennemi){
    while (Vector3.Distance(ennemi.transform.position, ennemi.cible.transform.position)>30f){
      
      float impatience = Random.Range(2f, 8f);
      yield return new WaitForSeconds(impatience);

    }

    ennemi.ChangerEtat(ennemi.chasse);
  }
  
}
