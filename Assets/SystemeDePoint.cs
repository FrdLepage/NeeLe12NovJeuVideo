using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemeDePoint : MonoBehaviour
{
    public Slider slider;

    public void SetMaxPoint(int point){
        slider.maxValue = point;
        slider.value = point;
    }
    
    public void SetPoint(int point){
        slider.value=point;
    }
}
