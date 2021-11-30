 using System.Collections;
using UnityEngine;

public class EnnemiEtatPromenade : EnnemiEtatsBase
{ 
  
  public override void InitEtat(EnnemiEtatsManager ennemi)
  {
      ennemi.StartCoroutine(anime(ennemi));
      ennemi.animator.SetBool("isWalking", true);
  }


  private IEnumerator anime(EnnemiEtatsManager ennemi){

    ennemi.agent.speed = 3f;

    //trouve la cible et la met en destination de L'agent
    ennemi.agent.destination = ennemi.origine.position;

    //path pending veut dire que ca a pas fini de calculer

    //tant que l'agent est a plus de 2.5 unite de la cible
    //ou bien que le path n'est pas encore calcule

    while(ennemi.agent.remainingDistance > 2.5f  || ennemi.agent.pathPending)
    {
        //met a jour toutes les 0.2 secondes
        yield return new WaitForSeconds(0.2f);
        
    }
    yield return new WaitForSeconds(1f);
    ennemi.animator.SetBool("isWalking", false);
    ennemi.ChangerEtat(ennemi.repos);
    yield return null;
  }
  
}
