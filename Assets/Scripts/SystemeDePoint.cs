using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemeDePoint : MonoBehaviour
{
    public Slider slider;
    public int currentPoint{ get; set;}
    public void SetMaxPoint(int point){
        slider.maxValue = point;
    }
    
    public void SetPoint(){
        slider.value=currentPoint;
        Debug.Log(slider.value);
    }
}
