using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSkybox : MonoBehaviour
{

    [SerializeField] private Color _color;
    [SerializeField] private GameObject _cristalValue;
    public float _speedRotation;

    void Start()
    {
        RenderSettings.skybox.SetFloat("_Exposure", 0.3f);

    }


    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * _speedRotation);
        if(RenderSettings.skybox.GetFloat("_Exposure") < 1){
            RenderSettings.skybox.SetFloat("_Exposure", 0.3f + (Time.time * (_cristalValue.GetComponent<SystemeDePoint>().slider.value)/25000));
        }
    }
}
