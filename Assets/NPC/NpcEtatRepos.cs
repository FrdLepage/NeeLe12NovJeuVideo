 using System.Collections;
using UnityEngine;

public class NpcEtatRepos : NpcEtatsBase
{ 
  
  public override void InitEtat(NpcEtatsManager npc)
  {
    //ennemi. pour avoir la reference au monobehavior
      npc.StartCoroutine(anime(npc));
  }
  

  private IEnumerator anime(NpcEtatsManager npc){

    
    while (Vector3.Distance(npc.transform.position, npc.cible.transform.position)>1f){

      float random = Random.Range(1f,50f);
      
      npc.agent.destination = npc.cible.transform.position + new Vector3(10,10,10);
      yield return new WaitForSeconds(1f);

    }

    

    
    // npc.ChangerEtat(npc.chasse);

  }
  
}
