using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemeDePoint : MonoBehaviour
{
    public Slider slider; //prendre le slider
    public int currentPoint{ get; set;}//Les points en ce moment
    public void SetMaxPoint(int point){//Cet que le max des point
        slider.maxValue = point;//le slider = sur le maximum des points
    }
    
    public void SetPoint(){
        slider.value=currentPoint;//changement selon les points courant
        Debug.Log(slider.value);//
    }
}
