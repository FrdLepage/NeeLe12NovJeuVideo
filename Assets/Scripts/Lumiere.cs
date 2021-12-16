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
    // Start is called before the first frame update
    void Start()
    {
        // on définie la lumiere qui sera utilisée
        _myLight = GetComponent<Light>();
        // on définie la valeur du temps
        _startTime=Time.time;
        
    }

    // Update is called once per frame
    void Update()
    {
            // on définie que la couleur change seulement si le joueur ramasse des cristaux
            float t = (Mathf.Sin(Time.time * (_cristalValue.GetComponent<SystemeDePoint>().slider.value)/10000));
            // on définie la couleur de départ et de fin de la lumiere
            _myLight.color = Color.Lerp(_startColor, _endColor, t);
        }
    }
