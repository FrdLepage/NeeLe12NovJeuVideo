using System.Collections;
using UnityEngine;

public class EnnemiEtatChasseRouge : EnnemiEtatsBaseRouge
{

    public override void InitEtat(EnnemiEtatsManagerRouge ennemi)
    {
        ennemi.StartCoroutine(anime(ennemi));
        ennemi.animator.SetBool("isRunning", true);
        Debug.Log(ennemi.cible.name);
    }


    private IEnumerator anime(EnnemiEtatsManagerRouge ennemi)
    {
        ennemi.agent.speed = 13f;
        Debug.Log(ennemi.cible.GetComponent<frederic_MovePerso>().feeExiste);

        ennemi.agent.SetDestination(ennemi.cible.transform.position);
        while (ennemi.agent.remainingDistance > 2f || ennemi.agent.pathPending)
        {
            ennemi.agent.SetDestination(ennemi.cible.transform.position);
            //met a jour toutes les 0.2 secondes
            yield return new WaitForSeconds(0.5f);
        }


        ennemi.animator.SetBool("isAttacking", true);
        yield return new WaitForSeconds(3f);
        ennemi.animator.SetBool("isAttacking", false);
        // ennemi.animator.SetBool("isRunning", false);
        ennemi.ChangerEtat(ennemi.promenade);



    }
    public override void TriggerEnterEtat(EnnemiEtatsManagerRouge ennemi, Collider other)
    {
        if(other.tag=="Fee"){
          Debug.Log("Sa marche tu??");
          ennemi.ChangerEtat(ennemi.promenade);
        }
    }

}
