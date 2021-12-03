 using System.Collections;
using UnityEngine;

public class EnnemiEtatChasseRouge : EnnemiEtatsBaseRouge
{ 
  
  public override void InitEtat(EnnemiEtatsManagerRouge ennemi)
  {
     ennemi.StartCoroutine(anime(ennemi));
     ennemi.animator.SetBool("isRunning", true);
  }
 

  private IEnumerator anime(EnnemiEtatsManagerRouge ennemi){
      ennemi.agent.speed = 13f;
  Debug.Log(ennemi.cible.GetComponent<frederic_MovePerso>().feeExiste);

      if(ennemi.cible.GetComponent<frederic_MovePerso>().feeExiste){
         ennemi.agent.destination = ennemi.cibleFee.transform.position;
      }
      else{
      ennemi.agent.destination = ennemi.cible.transform.position;  

      }

     

      //trouve la cible et la met en destination de L'agent

      // if(Vector3.Distance(ennemi.transform.position, ennemi.cibleFee.transform.position)>30f) {
      //   ennemi.agent.destination = ennemi.cibleFee.transform.position;
      // }else
      // {
      //   ennemi.agent.destination = ennemi.cible.transform.position;  
      // }


    //path pending veut dire que ca a pas fini de calculer

    //tant que l'agent est a plus de 2.5 unite de la cible
    //ou bien que le path n'est pas encore calcule

    // while(ennemi.agent.remainingDistance > 30f  || ennemi.agent.pathPending)
    // {
    //     //ajuste la destination sur la position de la cible
    //     //(utile si la cible est en mouvement)
    //     ennemi.agent.destination = ennemi.cible.transform.position;
    //     //met a jour toutes les 0.2 secondes
    //     yield return new WaitForSeconds(0.2f);
        
    // }
    ennemi.animator.SetBool("isAttacking", true);
    yield return new WaitForSeconds(3f);
    ennemi.animator.SetBool("isAttacking", false);
    // ennemi.animator.SetBool("isRunning", false);
    ennemi.ChangerEtat(ennemi.promenade);
    
  
  
  }
  
}
