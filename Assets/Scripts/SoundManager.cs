using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
 // Singleton *******************************

    //déclaration du composant AudioSource
    private AudioSource _audio;
    private static SoundManager _instance;

    public static SoundManager instance
    {
        get { return _instance; }
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // ***************************************

    private void Start()
    {
        //association du composant audio source
        _audio = GetComponent<AudioSource>();
    }


    /// <summary>
    /// Sert à jouer des son en précisant de ne pas
    /// partir un nouveau son s'il y en a deja un qui joue
    /// (si _audio n'est pas null)
    /// <summary>
    /// <param name="clip">Son à jouer</param>
    public void JouerSon(AudioClip clip)
    {
        if (_audio != null) _audio.PlayOneShot(clip);
    }
}
