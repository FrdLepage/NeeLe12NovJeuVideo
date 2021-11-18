using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Generateur : MonoBehaviour
{
    // [SerializeField] GameObject ennemi;
    [SerializeField] GameObject perso;
    public int largeurIle = 10;
    public int profondeurIle = 10;
    public GameObject cube;
    public Color newColor;
    private List<List<Material>> biomes = new List<List<Material>>();
    private List<List<GameObject>> items = new List<List<GameObject>>();

    public Renderer textureRenderer;
    public float attenuateur;
    
    private Object mats; 
    private GameObject its; 

    public int coefAltitude = 10;
    // public Color[] biomesCouleurs;

    
    private void Start(){
        GenererListeMaterielsBiomes();
        GenererListeItems();
        CreerMap();
        GetComponent<NavMeshSurface>().BuildNavMesh();
    }

    void GenererListeItems()
    {
        //premier parametre du nom du fichier
        int numeroBiome = 1;

        //deuxieme parametre du nom du fichier
        int nbVariants = 1;

        //est vrai quand il reste des matériaux
        bool resteDesItems = true;

        List<GameObject> tpItems = new List<GameObject>();

        do{
            //aller cherche l'objet dans resources
            //load les ressouces avec le bon nom
            its = Resources.Load("items/i_" + numeroBiome + "_" + nbVariants) as GameObject;

            //si l'objet n'est pas null
            if(its){
                //ajout du materiel its dans la liste tpBiome
                tpItems.Add((GameObject)its);
                //le nombre de variant augmente
                nbVariants++;
            }
            else
            {
                //si variant est egal 1, ca veut dire qu'il n'a pas ete ajouter en haut, donc il n'y a plus de materiels
                if(nbVariants == 1){
                    resteDesItems = false;

                }
                //on ajout la liste a la liste de liste
                items.Add(tpItems);
                //on remet la liste temporaire a 0
                tpItems = new List<GameObject>();
                //on augmente le nombre de biomes
                numeroBiome++;
                //le nb de variant retourne a 1
                nbVariants = 1;

            }


        }
        //tant qu'il y a des materiaux a ajouter
        while (resteDesItems);

    }

    void GenererListeMaterielsBiomes()
    {
        //premier parametre du nom du fichier
        int nbBiomes = 1;

        //deuxieme parametre du nom du fichier
        int nbVariants = 1;

        //est vrai quand il reste des matériaux
        bool resteDesMats = true;

        List<Material> tpBiome = new List<Material>();

        do{
            //aller cherche l'objet dans resources
            //load les ressouces avec le bon nom
            mats = Resources.Load("materiaux/b" + nbBiomes + "_" + nbVariants);

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

    void CreerMap(){

        float[,] map = GenererInnondationCirculaire(profondeurIle, largeurIle);
        float[,] ile = GenererTerrain(profondeurIle, largeurIle, attenuateur, map);
        DessinerMap(ile);
        GenererIle(ile);

    }

    float Sigmoid(float value){
        float k =20f;
        float c= 0.7f;
        return 1/(1 + Mathf.Exp(-k *(value-c)));

    }

    private float[,] GenererBordureEau(int maxZ, int maxX){
        float[,] ocean = new float [maxZ, maxX];
        //a modifier pour avoir une ile circulaire
        float centreX = maxX/2;
        float centreZ = maxZ/2;


        for (int z = 0; z < maxZ; z++)
        {
            for (int x = 0; x < maxX; x++)
            {
                float yx = Mathf.Abs(x-centreX)/centreX;
                float yz = Mathf.Abs(z-centreZ)/centreZ;
                float y = Mathf.Max(yx, yz);

                ocean[z,x] = Sigmoid(y);
            }
        }

        return ocean;

    }


    private float[,] GenererTerrain(int maxZ, int maxX, float attenuateur, float[,] innondation){
         //on peut aussi faire une liste
        //pour avoir une map differente a chaque fois qu'on fait play
        int bruitAleatoire = Random.Range(0,9000);
        float[,] terrain = new float [maxZ, maxX];
        

         for (int z = 0; z < maxZ; z++)
        {
            for (int x = 0; x < maxX; x++)
            {
                // float y = Random.Range(0f, 1f);
                float y = Mathf.PerlinNoise(x/attenuateur + bruitAleatoire, z/attenuateur + bruitAleatoire);
                float yflood = innondation[z,x];
                
                terrain [z, x] = Mathf.Clamp01(y - yflood);               

                //GameObject unCube = Instantiate(cube, new Vector3(x,y,z), Quaternion.identity);
            }
        }
       return terrain;

    }

    

        // Color couleur = new Color(y, y, y, 1f);

        // couleursTexture[z * largeurIle + x] = couleur; // remplir un tableau a une dimension dans une double boucle
 

    private List <string> biomeDepart = new List<string>(){"d1_1","d1_2","d1_3","d1_4"};
    void GenererIle(float[,] map){
      

        int larg = map.GetLength(0);
        int prof = map.GetLength(1);
        int maximum= biomes.Count-1;
        Debug.Log(maximum);
        //tirage de 1/20
        int tirage = 1;
        int tirageRouge = 1;
        int allier = 1;
        int npc = 1;

        for (int z = 0; z < prof; z++)
        {
            for (int x = 0; x < larg; x++)
            {
                float y = map[z,x];
                if(y > 0f){
                int quelBiome = Mathf.FloorToInt(map[z,x] * maximum);
                GameObject unCube = Instantiate(cube, new Vector3(x, y * coefAltitude, z), Quaternion.identity);
                int variantItem = Random.Range(0,items[quelBiome].Count-1);
               
                int SpawnItem = Random.Range(0,15);
                if(SpawnItem==1&&quelBiome<4){
                GameObject unItem = Instantiate(items[quelBiome][variantItem],new Vector3( unCube.transform.position.x,unCube.transform.position.y,unCube.transform.position.z),Quaternion.identity);
                unCube.GetComponent<BiomesEtatsManager>().biomeItem = unItem;
                }


                int variantDepart = Random.Range(0,biomeDepart.Count-1);

                unCube.GetComponent<Renderer>().material = Resources.Load("materiaux/" + biomeDepart[variantDepart]) as Material;
                

               

                int quelVariant = Random.Range(0,(biomes[quelBiome].Count-1));
           
                //mettre la couleur correspondante selon le biome
                //floor to int arrondi au plus petit...ex: 7,8=7
                
                unCube.GetComponent<BiomesEtatsManager>().biomeMateriel=biomes[quelBiome][quelVariant];

                unCube.AddComponent<BoxCollider>();


                // if(Random.Range(1,100) > 98)
                // {
                //     //instancier ennemi sur le cube +1 en y
                //     GameObject unAgent = Instantiate((GameObject)Resources.Load("ennemis/ennemi"), new Vector3(unCube.transform.position.x, unCube.transform.position.y+1, unCube.transform.position.z), Quaternion.identity);
                //     unAgent.GetComponent<EnnemiEtatsManager>().cible = perso;
                //     unAgent.GetComponent<EnnemiEtatsManager>().origine = unCube.transform;
                // }

                
               
                //si gagne tirage
                Vector3 position = new Vector3(0,0,0);
                tirage++;
                allier++;
                npc++;
                tirageRouge++;

                if(tirage == 3000){

                GameObject ennemi = Instantiate((GameObject)Resources.Load("ennemis/ennemi"));
                // unAgent.GetComponent<EnnemiEtatsManager>().cible = perso;
                // unAgent.GetComponent<EnnemiEtatsManager>().origine = unCube.transform;
                //trouver la position du cube
                position = unCube.transform.position;
                //spawn prefab at position
                Instantiate(ennemi, position, Quaternion.identity);
                tirage = 0;
                }

                if(npc == 3500){

                position = unCube.transform.position;
                GameObject Npc = Instantiate((GameObject)Resources.Load("NPC"), new Vector3(position.x, position.y, position.z), Quaternion.identity);
          
                //trouver la position du cube
                //spawn prefab at position
                Npc.GetComponent<NpcEtatsManager>().cible = perso;
                Npc.GetComponent<NpcEtatsManager>().origine = unCube.transform;
                npc = 0;
                }





                if(allier == 2000){

                GameObject unAllier = Instantiate((GameObject)Resources.Load("feeSpawn"), new Vector3(unCube.transform.position.x, unCube.transform.position.y, unCube.transform.position.z), Quaternion.identity);
                unAllier.GetComponent<AllierEtatManager>().cible = perso;
                unAllier.GetComponent<AllierEtatManager>().origine = unCube.transform;
                //trouver la position du cube
                position = unCube.transform.position;
                allier = 0;
                }


                // tirageRouge++;

                if(tirageRouge == 2200){

                GameObject unAgent = Instantiate((GameObject)Resources.Load("ennemis/goblin_rouge"), new Vector3(unCube.transform.position.x, unCube.transform.position.y+1, unCube.transform.position.z), Quaternion.identity);
                unAgent.GetComponent<EnnemiEtatsManagerRouge>().cible = perso;
                unAgent.GetComponent<EnnemiEtatsManagerRouge>().origine = unCube.transform;
                //trouver la position du cube
                position = unCube.transform.position;
                //spawn prefab at position
                // Instantiate(ennemi, position, Quaternion.identity);
                tirageRouge = 0;
                }
                
                
                unCube.transform.parent = transform;
               
                
                }
            }
        }
        }
    

    void DessinerMap(float[,] map){
        
        //pour avoir une map differente a chaque fois qu'on fait play
        int bruitAleatoire = Random.Range(0, 100000);

        int larg = map.GetLength(0);
        int prof = map.GetLength(1);

        Texture2D ileTexture = new Texture2D(larg, prof);
        Color[] couleursTexture = new Color[larg * prof]; //on peut aussi faire une liste

        for (int z = 0; z < prof; z++)
        {
            for (int x = 0; x < larg; x++)
            {
                float y = map[z,x];
                Color couleur = new Color(y, y, y, 1f);

                couleursTexture[z * largeurIle + x] = couleur; // remplir un tableau a une dimension dans une double boucle
            }
        }
        
       
        //dessine la texture
        // ileTexture.SetPixels(couleursTexture);
        // ileTexture.Apply();
        // textureRenderer.sharedMaterial.mainTexture = ileTexture;
        //la texture est transformer selon les dimensions de l'ile
        textureRenderer.transform.localScale = new Vector3(largeurIle, 1f, profondeurIle);

    }

    private float[,] GenererInnondationCirculaire(int maxX, int maxZ){

        float[,] oceanRond = new float [maxZ, maxX];

        float cX = maxX/2;
        float cZ = maxZ/2;

        //centre du cercle
        Vector2 Centre = new Vector2 (cX, cZ); 

        for (int z = 0; z < maxZ; z++)
        {
            for (int x = 0; x < maxX; x++)
            {
                float rayon = Vector2.Distance(Centre, new Vector2 (x,z));

                
                float val = rayon/cX;

                oceanRond[z,x] = Sigmoid(val);
            }
            
        }

        return oceanRond;
        
    }

}