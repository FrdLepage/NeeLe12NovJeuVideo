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
    while (Vector3.Distance(ennemi.transform.position, ennemi.cible.transform.position)>30f){//si l'ennemis se trouve a une distance de 30 unite de la cible se dirige vers le personnage
      
      float impatience = Random.Range(1f, 3f);
      yield return new WaitForSeconds(impatience);

    }

    ennemi.ChangerEtat(ennemi.chasse);
  }
  public override void TriggerEnterEtat(EnnemiEtatsManagerRouge ennemi, Collider other)
    {
        
    }

}
