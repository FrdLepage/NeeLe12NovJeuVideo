 using System.Collections;
using UnityEngine;

public class NpcEtatPromenade : NpcEtatsBase
{ 
  
  public override void InitEtat(NpcEtatsManager npc)
  {
      npc.StartCoroutine(anime(npc));
      
  }


  private IEnumerator anime(NpcEtatsManager npc){

    npc.agent.speed = 3f;

    //trouve la cible et la met en destination de L'agent
    npc.agent.destination = npc.origine.position;
    //path pending veut dire que ca a pas fini de calculer

    //tant que l'agent est a plus de 2.5 unite de la cible
    //ou bien que le path n'est pas encore calcule

    while(npc.agent.remainingDistance > 2.5f  || npc.agent.pathPending)
    {
        //met a jour toutes les 0.2 secondes
        yield return new WaitForSeconds(0.2f);
        
    }
    yield return new WaitForSeconds(1f);
  
    npc.ChangerEtat(npc.repos);
    yield return null;
  }
  
}
