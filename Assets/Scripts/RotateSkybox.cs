using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSkybox : MonoBehaviour
{
    [SerializeField] private Color _color;
    [SerializeField] private GameObject _cristalValue;
    public float _speedRotation;
    
    // Start is called before the first frame update
    void Start()
    {
        // on va chercher la valeur de l'exposition du skybox et on met sa valeur initial a 0.3 
        RenderSettings.skybox.SetFloat("_Exposure", 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        // on va chercher la valeur de la rotation du skybox et on la multiplie avec la valeur de _speedRotation pour faire tourner le skybox 
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * _speedRotation);

        // on dit a l'exposition du skybox d'augmenter seulement quand le joueur ramasse des cristaux(_cristalValue.GetComponent<SystemeDePoint>().slider.value)
        // l'exposition augmente si sa valeur est plus petite que 1 
        if(RenderSettings.skybox.GetFloat("_Exposure") < 1){
            // et elle augmente si sa valeur est plus grande que 0.3
            RenderSettings.skybox.SetFloat("_Exposure", 0.3f + (Time.time * (_cristalValue.GetComponent<SystemeDePoint>().slider.value)/20000));
        }
    }
}
