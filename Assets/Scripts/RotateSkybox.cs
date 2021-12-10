using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSkybox : MonoBehaviour
{

    [SerializeField] private Color _color;

    public float _speedRotation;
    public float _speedExposure;
    // public float _speedTint;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * _speedRotation);
        RenderSettings.skybox.SetFloat("_Exposure", Time.time * _speedExposure);
        // RenderSettings.skybox.SetColor("_Tint", Color.red);
    }
}
