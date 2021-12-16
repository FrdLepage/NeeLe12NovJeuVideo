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
        ennemi.agent.speed = 13f;// le speed de l'ennemis

        ennemi.agent.SetDestination(ennemi.cible.transform.position);//l'ennemi se dirige vers le personnage
        while (ennemi.agent.remainingDistance > 2f || ennemi.agent.pathPending)//l'ennemi continu de se diriger a moins qui soit a coter du personnage
        {
            ennemi.agent.SetDestination(ennemi.cible.transform.position);//l'ennemi se dirige vers le personnage
            //met a jour toutes les 0.2 secondes
            yield return new WaitForSeconds(0.5f);
        }


        ennemi.animator.SetBool("isAttacking", true);//animation d'attaquer
        yield return new WaitForSeconds(3f);
        ennemi.animator.SetBool("isAttacking", false);
        // ennemi.animator.SetBool("isRunning", false);
        ennemi.ChangerEtat(ennemi.promenade);//change l'eta



    }
    public override void TriggerEnterEtat(EnnemiEtatsManagerRouge ennemi, Collider other)
    {
        if(other.tag=="Fee"){//si l'ennemis est en trigger avec la fee change ton etat a promenade
          Debug.Log("Sa marche tu??");
          ennemi.ChangerEtat(ennemi.promenade);
        }
    }

}
