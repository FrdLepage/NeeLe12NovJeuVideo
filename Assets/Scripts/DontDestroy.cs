using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    void Awake()
    {
        //Évite que le Sound Manager soit détruit lorsqu'on change de scène
        DontDestroyOnLoad(gameObject);
    }
}
