 using System.Collections;
using UnityEngine;

public class EnnemiEtatReposRouge : EnnemiEtatsBaseRouge
{ 
  
  public override void InitEtat(EnnemiEtatsManagerRouge ennemi)
  {
    //ennemi. pour avoir la reference au monobehavior
      ennemi.StartCoroutine(anime(ennemi));
  }
  

  private IEnumerator anime(EnnemiEtatsManagerRouge ennemi){
    while (Vector3.Distance(ennemi.transform.position, ennemi.cible.transform.position)>30f){
      
      float impatience = Random.Range(2f, 8f);
      yield return new WaitForSeconds(impatience);

    }

    ennemi.ChangerEtat(ennemi.chasse);
  }
  
}