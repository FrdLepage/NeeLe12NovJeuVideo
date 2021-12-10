using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSkybox : MonoBehaviour
{

    [SerializeField] private Color _color;
    [SerializeField] private GameObject _cristalValue;

    public float _speedRotation;
    // public float _speedExposure;
    // public float _speedTint;
    
    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.skybox.SetFloat("_Exposure", 0.2f);

    }


    // Update is called once per frame
    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * _speedRotation);

        if(RenderSettings.skybox.GetFloat("_Exposure") < 1){
            RenderSettings.skybox.SetFloat("_Exposure", 0.2f + (Time.time * (_cristalValue.GetComponent<SystemeDePoint>().slider.value)/25000));
        }
        // Debug.Log(_cristalValue.GetComponent<SystemeDePoint>().slider.value);
        // RenderSettings.skybox.SetColor("_Tint", Color.red);
    }
}
