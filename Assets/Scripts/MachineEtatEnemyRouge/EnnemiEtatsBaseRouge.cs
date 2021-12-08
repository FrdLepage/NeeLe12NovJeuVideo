using UnityEngine;

public abstract class EnnemiEtatsBaseRouge
{
  public abstract void InitEtat(EnnemiEtatsManagerRouge ennemi);
  public abstract void TriggerEnterEtat(EnnemiEtatsManagerRouge ennemi, Collider other);
}
