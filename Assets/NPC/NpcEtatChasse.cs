 using System.Collections;
using UnityEngine;

public class NpcEtatChasse : NpcEtatsBase
{ 
  
  public override void InitEtat(NpcEtatsManager npc)
  {
     npc.StartCoroutine(anime(npc));
    //  npc.animator.SetBool("isRunning", true);
  }
 

  private IEnumerator anime(NpcEtatsManager npc){
    Debug.Log("coroutine chasse");
    npc.agent.speed = 13f;
    
      //trouve la cible et la met en destination de L'agent

    npc.agent.destination = npc.cible.transform.position;

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
    // ennemi.animator.SetBool("isAttacking", true);
    yield return new WaitForSeconds(3f);
    // ennemi.animator.SetBool("isAttacking", false);
    // // ennemi.animator.SetBool("isRunning", false);
    // ennemi.ChangerEtat(ennemi.promenade);
    
  
  
  }
  
}
