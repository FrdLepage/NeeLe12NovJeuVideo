using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Generateur : MonoBehaviour
{
    // [SerializeField] GameObject ennemi;
    [SerializeField] GameObject perso;
    [SerializeField] AudioClip fin;
    [SerializeField] Material eau;
    [SerializeField] Material matFin;
    [SerializeField] Material lave;
    
    public int largeurIle = 10;
    public int profondeurIle = 10;
    public GameObject cube;
    public bool finJeu = false;
    public Color newColor;
    private List<List<Material>> biomes = new List<List<Material>>();
    private List<List<GameObject>> items = new List<List<GameObject>>();

    public Renderer textureRenderer;
    public GameObject PlaneDeLave;
   
    public Slider sliderTemps;
    public int currentTime;
    public float attenuateur;
     public Component[] Rend;
    public GameObject point;
    private Object mats; 
    private GameObject its; 
    public int coefAltitude = 10;
    // public Color[] biomesCouleurs;

    
    private void Start(){
        GenererListeMaterielsBiomes();
        GenererListeItems();
        CreerMap();
        GetComponent<NavMeshSurface>().BuildNavMesh();
        InvokeRepeating("Timer",1.0f,1.0f);
        textureRenderer.GetComponent<Renderer>().material = lave;
        currentTime=500;
        sliderTemps.maxValue = currentTime;
        Invoke("MettreCollider", 2f);   
    }

    private void Timer() {
        if(currentTime==0){
            SceneManager.LoadScene("GameOver");
        }else{
        currentTime--;
        sliderTemps.value=currentTime;
        }
    }

    
    /// <summary>
    /// Fonction qui permet d'aller chercher tous les items presents dans le dossier
    /// resources et de faire une liste avec, dans laquelle se trouve une autre liste
    /// contenant les differentes options d'items pour ce biome.
    /// </summary>
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
                //ajout du game object its dans la liste tpBiome
                tpItems.Add((GameObject)its);
                //le nombre de variant augmente
                nbVariants++;
            }
            else
            {
                //si variant est egal 1, ca veut dire qu'il n'a pas ete ajouter en haut, donc il n'y a plus d'items
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

    /// <summary>
    /// Fonction qui permet d'aller chercher tous les materiaux presents dans le dossier
    /// resources et de faire une liste avec, dans laquelle se trouve une autre liste
    /// contenant les differentes options de ce meme materiel.
    /// </summary>
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

    void Update()
    {
        if(point.GetComponent<SystemeDePoint>().slider.value == 200){
            StartCoroutine(RetourIle());
        }
    }
    
    /// <summary>
    /// Permet de la transformation de l'île a la fin d'une partie réussie
    /// change le plane de lave en eau et change le materiel des biomes
    /// </summary>
    private IEnumerator RetourIle(){
        SoundManager.instance.JouerSon(fin); //pour jouer le son
        textureRenderer.GetComponent<Renderer>().material = eau; //pour appliquer le materiel de l'eau sur le plane
        //pour mettre tous les composants render de tous les cubes dans le tableau Rend
        Rend = this.GetComponentsInChildren<Renderer>();
        //pour chaque element du tableau, on associe le materiel de la fin
        foreach (Renderer rend in Rend) rend.material = matFin;
        //delai de 3 secondes
        yield return new WaitForSeconds(3f);
        //pour charger la scene de fin
        SceneManager.LoadScene("Gagne");
        yield return null;
    }
    /// <summary>
    /// Fonction qui appelle toutes les fonctions necessaires afin de creer la map
    /// </summary>
    void CreerMap(){
        //les valeurs du tableau 2d map contenant les valeurs pour les dimensions de la map sont calculees grace a la fonction GenererInnondationCirculaire
        float[,] map = GenererInnondationCirculaire(profondeurIle, largeurIle);
        //les valeurs du tableau 2d ile contenant les valeurs pour les dimensions de l'ile sont calculees grace a la fonction GenererTerrain
        float[,] ile = GenererTerrain(profondeurIle, largeurIle, attenuateur, map);
        //appel de la fonction pour dessiner la map
        DessinerMap(ile);
        //appel de la fonction pour generer l'ile
        GenererIle(ile);
    }

    private void MettreCollider(){
        PlaneDeLave.GetComponent<BoxCollider>().enabled = true;
    }
    /// <summary>
    /// La fonction sigmoide permet faire le calcul mathematique qui permet de creer le degrade autour de l'ile
    /// </summary>
    float Sigmoid(float value){
        //la valeur de k correspond a l'intensite de la pente
        float k =20f;
        //correspond a l'espace occupe par l'ile
        float c= 0.7f;
        //pour retourner le calcul de la fonction sigmoid
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

    /// <summary>
    /// Fonction qui retourne les dimensons que doit avoir le terrain afin d'y generer l'ile
    /// </summary>
    /// <param name="maxX">correspond a la valeur maximum en x pour les dimensions du terrain </param>
    /// <param name="maxZ">correspond a la valeur maximum en z pour les dimensions du terrain </param>
    /// <param name="attenuateur">permet de gerer l'espacement entre les cubes</param>
    /// <param name="innondation">contient les valeurs de la map </param>
    private float[,] GenererTerrain(int maxZ, int maxX, float attenuateur, float[,] innondation){
        //permet d'ajouter un facteur aleatoire
        int bruitAleatoire = Random.Range(0,9000);
        //teableau 2d qui contient les valeurs pour le terrain en fonction de la largeur et de la profondeur de L'ile
        float[,] terrain = new float [maxZ, maxX];
        
        //boucle en z de toutes les valeurs en z du terrain
         for (int z = 0; z < maxZ; z++)
        {
            //boucle en x pour toutes les valeurs en x du terrain
            for (int x = 0; x < maxX; x++)
            {
               
                // permet de definir la hauteur des cubes. La fonction PerlinNoise permet de creer un modele aleatoire en prenant les deux valeurs en parametre soit
                //la valeur en x divisee par l'attenuateur pour eviter d'avoir trop d'espace entre les cubes et la valeur en z aussi divisee par l'attenuateur
                float y = Mathf.PerlinNoise(x/attenuateur + bruitAleatoire, z/attenuateur + bruitAleatoire);
                 // associe les valeurs du tableau 2D innondation au float yflood
                float yflood = innondation[z,x];
                //permet de garder entre 0 et 1 les valeurs du terrain en y                
                terrain [z, x] = Mathf.Clamp01(y - yflood);               
            }
        }
       return terrain;
    }
    private List <string> biomeDepart = new List<string>(){"d1_1","d1_2","d1_3","d1_4"};

    /// <summary>
    /// Fonction qui permet de generer l'ile avec les cubes
    /// </summary>
    /// <param name="maxX">tableau 2d contenant les valeurs de la map </param>
    void GenererIle(float[,] map){
        //pour aller chercher la valeur de la largeur de l'ile
        int larg = map.GetLength(0);
        // pour aller cherche la valeur de la profondeur de l'ile
        int prof = map.GetLength(1);
        //le nombre maximum de biomes
        int maximum= biomes.Count-1;

        int tirageRouge = 1; //valeur qui determine si un ennemi rouge sera instancier
        int tiragePotion = 1;//valeur qui determine si la potion sera instancier

         //boucle en z pour toutes les valeurs de la profondeur de la map
        for (int z = 0; z < prof; z++)
        {
             //boucle en x pour toutes les valeur de la largeur de la map
            for (int x = 0; x < larg; x++)
            {
                //coordonne en y du cube
                float y = map[z,x];
                if(y > 0f){
                //permet de determiner quel biome sera choisi en fonction du nombre maximum de biomes presents et de la valeur de la map en x et en z et d'arrondir
                //la valeur trouvee
                int quelBiome = Mathf.FloorToInt(map[z,x] * maximum);
                //pour instancier le cube a sa position selon la valeur en x et en z et selon l'altitude
                GameObject unCube = Instantiate(cube, new Vector3(x, y * coefAltitude, z), Quaternion.identity);
                //pour determiner quel au hasard quel variant sera sur le cube
                int variantItem = Random.Range(0,items[quelBiome].Count-1);
               
                int SpawnItem = Random.Range(0,15);
                if(SpawnItem==1&&quelBiome<4){
                GameObject unItem = Instantiate(items[quelBiome][variantItem],new Vector3( unCube.transform.position.x,unCube.transform.position.y+0.5f,unCube.transform.position.z),Quaternion.identity);
                unCube.GetComponent<BiomesEtatsManager>().biomeItem = unItem;
                }
                unCube.GetComponent<BiomesEtatsManager>().point = point;

                int variantDepart = Random.Range(0,biomeDepart.Count-1);

                unCube.GetComponent<Renderer>().material = Resources.Load("materiaux/" + biomeDepart[variantDepart]) as Material;
                int quelVariant = Random.Range(0,(biomes[quelBiome].Count-1));
           
                //permet d'appliquer le bon biome en allant chercher le composant BionesEtatsManager qui apparatient au cube et en choisissant le bon biome
                //et le bon variant dans la liste de biomes      
                unCube.GetComponent<BiomesEtatsManager>().biomeMateriel=biomes[quelBiome][quelVariant];
                //ajoute un composant BoxCollider au cube
                unCube.AddComponent<BoxCollider>();        
               //declaration de la position pour le cube
                Vector3 position = new Vector3(0,0,0);
                tirageRouge++;//valeur pour determiner si un ennemi rouge sera instanciee augmente
                tiragePotion++; //valeur pour determiner si une potion sera instanciee augmente
              
                //si tirage potion atteint 10 000
                if(tiragePotion == 10000){
                    //pour aller cherche la potion dans le dossier ressources
                    GameObject potion = ((GameObject)Resources.Load("potion"));
                    //position du cube
                    position = unCube.transform.position;
                    //instancier la potion en haut du cube
                    Instantiate(potion, new Vector3(position.x,position.y+2,position.z), Quaternion.identity);
                    //remise a 0 du tiragePotion
                    tiragePotion = 0;
                }

                //si tirageRouge atteint 4200
                if(tirageRouge == 4200){
                //pour aller cherche l'ennemi rouge dans le dossier ressources et l'instancier sur le cube
                GameObject unAgent = Instantiate((GameObject)Resources.Load("ennemis/goblin_rouge"), new Vector3(unCube.transform.position.x, unCube.transform.position.y+3, unCube.transform.position.z), Quaternion.identity);
                //la cible pour l'agent est le perso
                unAgent.GetComponent<EnnemiEtatsManagerRouge>().cible = perso;
                //l'orginie pour l'ennemi correspond a la position du cube    
                unAgent.GetComponent<EnnemiEtatsManagerRouge>().origine = unCube.transform;
                //trouver la position du cube
                position = unCube.transform.position;
                //tirageRouge est remis a 0
                tirageRouge = 0;
                }
                //faire en sorte que les cube soient enfants de l'objet Generateur
                unCube.transform.parent = transform;
                }
            }
        }
    }
    
    /// <summary>
    /// Permet de dessiner la representation visuelle de la map en noir et blanc
    /// </summary>
    /// <param name="map">tableau 2D qui correspond au valeurs de la map </param>
    void DessinerMap(float[,] map){
        
        //pour avoir une map differente a chaque fois qu'on fait play
        int bruitAleatoire = Random.Range(0, 100000);
        //pour aller chercher la valeur de la largeur de l'ile
        int larg = map.GetLength(0);
         //pour aller chercher la valeur de la profondeur de l'ile
        int prof = map.GetLength(1);
        Texture2D ileTexture = new Texture2D(larg, prof);
        Color[] couleursTexture = new Color[larg * prof];

        for (int z = 0; z < prof; z++)
        {
            for (int x = 0; x < larg; x++)
            {
                //vient prendre le tableau map pour le mettre dans le y
                float y = map[z,x];
                //determine la couleur
                Color couleur = new Color(y, y, y, 1f);
                //pour remplir le tableau couleursTexture avec la couleur
                couleursTexture[z * largeurIle + x] = couleur; // remplir un tableau a une dimension dans une double boucle
            }
        }
        //la texture est transformer selon les dimensions de l'ile
        textureRenderer.transform.localScale = new Vector3(largeurIle, 1f, profondeurIle);

    }
    /// <summary>
    /// Permet de calculer les valeurs pour former une ile ronde
    /// </summary>
    /// <param name="maxX">correspond a la valeur maximum en x pour les dimensions de la map </param>
    /// <param name="maxZ">correspond a la valeur maximum en z pour les dimensions de la map </param>
    private float[,] GenererInnondationCirculaire(int maxX, int maxZ){
        //tableau 2D contenant les valeur en x et en z pour l'ile ronde
        float[,] oceanRond = new float [maxZ, maxX];
        //divsion de la valeur de la largeur pour trouver le centre
        float cX = maxX/2;
        //division de la valeur de la profondeur pour trouver le centre
        float cZ = maxZ/2;
        // vecteur qui correspond au centre du cercle
        Vector2 Centre = new Vector2 (cX, cZ); 

        for (int z = 0; z < maxZ; z++)
        {
            for (int x = 0; x < maxX; x++)
            {
                //permet de trouver le rayon du cercle en calculant la distance entre le centrer et le point en x et en z
                float rayon = Vector2.Distance(Centre, new Vector2 (x,z));
                // pour ramener la valeur entre 0 et 1                
                float val = rayon/cX;
                 //l'appel de la sigmoid permet de creer un degrade plus intense
                oceanRond[z,x] = Sigmoid(val);
            }
        }
         //retourne la valeur de l'ile ronde
        return oceanRond;
        
    }

}
