 using System.Collections;
using UnityEngine;

public class EnnemiEtatChasse : EnnemiEtatsBase
{ 
     
  
  public override void InitEtat(EnnemiEtatsManager ennemi)
  {
     ennemi.StartCoroutine(anime(ennemi));
     ennemi.animator.SetBool("isRunning", true);
  }
 

  private IEnumerator anime(EnnemiEtatsManager ennemi){
      ennemi.agent.speed = 13f;
    
      //trouve la cible et la met en destination de L'agent
    ennemi.agent.destination = ennemi.cible.transform.position;

    //path pending veut dire que ca a pas fini de calculer

    //tant que l'agent est a plus de 2.5 unite de la cible
    //ou bien que le path n'est pas encore calcule

    while(ennemi.agent.remainingDistance > 2.5f  || ennemi.agent.pathPending)
    {
        //ajuste la destination sur la position de la cible
        //(utile si la cible est en mouvement)
        ennemi.agent.destination = ennemi.cible.transform.position;
        //met a jour toutes les 0.2 secondes
        yield return new WaitForSeconds(0.2f);
        
    }
  
    ennemi.animator.SetBool("isAttacking", true);
    yield return new WaitForSeconds(2.8f);
    GameObject boule = GameObject.Instantiate((GameObject)Resources.Load("BouleFeu"), new Vector3(ennemi.transform.position.x, ennemi.transform.position.y+2, ennemi.transform.position.z), Quaternion.identity);
    yield return new WaitForSeconds(1f);
    //lancer la boule

    yield return new WaitForSeconds(3f);
    ennemi.animator.SetBool("isAttacking", false);
    // ennemi.animator.SetBool("isRunning", false);
    ennemi.ChangerEtat(ennemi.promenade);
    
  
  
  }
  
}
