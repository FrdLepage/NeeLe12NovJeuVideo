using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomesEtatsManager : MonoBehaviour
{

    [SerializeField] public Material matParticules;
    private BiomesEtatsBase etatActuel;
    public BiomesEtatActivable activable = new BiomesEtatActivable();
    public BiomesEtatCultivable cultivable = new BiomesEtatCultivable();

    public Material biomeMateriel{ get; set;}

    public GameObject biomeItem {get;set;}

    // Start is called before the first frame update
    void Start()
    {
        ChangerEtat(activable);
    }

    public void ChangerEtat(BiomesEtatsBase etat)
    {
        
        etatActuel = etat;
        etatActuel.InitEtat(this);
    }
    
    // Update is called once per frame
    void Update()
    {
        etatActuel.UpdateEtat(this);
    }

    
    private void OnTriggerEnter(Collider other)
    {
        
        etatActuel.TriggerEnterEtat(this,other);
    }


}
