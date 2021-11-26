using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllierEtatChasse : AllierEtatBase
{
    
    public override void InitEtat(AllierEtatManager allier)
    {
        allier.StartCoroutine(direction(allier));
        
    }

    private IEnumerator direction(AllierEtatManager allier){
      allier.agent.speed = 13f;
    
      //trouve la cible et la met en destination de L'agent
      allier.agent.destination = allier.cible.transform.position;


    
    yield return new WaitForSeconds(3f);

    }
}
