using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ressources_test : MonoBehaviour
{
    
    
    //liste de liste pour contenir les biomes
    public List<List<Material>> biomes = new List<List<Material>>();

    //liste temporaire contenant les materiaux
    public List<Material> tpBiome = new List<Material>();

    private GameObject go;

    //declaration de mats comme etant un objet
    private Object mats;

    // Start is called before the first frame update
    void Start()
    {
        //premier parametre du nom du fichier
        int nbBiomes = 1;

        //deuxieme parametre du nom du fichier
        int nbVariants = 1;

        //est vrai quand il reste des matériaux
        bool resteDesMats = true;

        do{
            //aller cherche l'objet dans resources
            //load les ressouces avec le bon nom
            mats = Resources.Load("b" + nbBiomes + "_" + nbVariants);

            //si l'objet n'est pas null
            if(mats){
                //ajout du materiel mats dans la liste tpBiome
                tpBiome.Add((Material)mats);
                //le nombre de variant augmente
                nbVariants++;
            }
            else
            {
                //si variant est egal 1, ca veut dire qu'il n'a pas ete ajouter en haut, donc il n'y a plus de materiels
                if(nbVariants == 1){
                    resteDesMats = false;

                }
                //on ajout la liste a la liste de liste
                biomes.Add(tpBiome);
                //on remet la liste temporaire a 0
                tpBiome = new List<Material>();
                //on augmente le nombre de biomes
                nbBiomes++;
                //le nb de variant retourne a 1
                nbVariants = 1;

            }


        }
        //tant qu'il y a des materiaux a ajouter
        while (resteDesMats);

    }
    
}
