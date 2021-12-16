using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lumiere : MonoBehaviour
{
    [SerializeField] private GameObject _cristalValue;
    public Light _myLight;
    public Color _startColor;
    public Color _endColor;
    public bool _changeColor = false;
    float _startTime;
    public bool _repeatColor;
    // Start is called before the first frame update
    void Start()
    {
        _myLight = GetComponent<Light>();
        _startTime=Time.time;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_changeColor){

            float t = (Mathf.Sin(Time.time * (_cristalValue.GetComponent<SystemeDePoint>().slider.value)/10000));
            _myLight.color = Color.Lerp(_startColor, _endColor, t);
            }
        }
    }
