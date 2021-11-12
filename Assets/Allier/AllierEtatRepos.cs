using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllierEtatRepos : AllierEtatBase
{
    // Start is called before the first frame update
    public override void InitEtat(AllierEtatManager allier)
  {
    allier.StartCoroutine(direction(allier));
  }  

  private IEnumerator direction(AllierEtatManager allier){
      while (Vector3.Distance(allier.transform.position, allier.cible.transform.position)>30f){
      
      yield return null;

    }

    allier.ChangerEtat(allier.chasse);
  }
}

