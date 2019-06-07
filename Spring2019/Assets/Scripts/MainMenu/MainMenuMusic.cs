using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuMusic : MonoBehaviour
{
    public AudioSource music; 
    public AudioSource audioSXF;

    public void PlayStartSFX()
    {
        music.enabled = false; 
        audioSXF.enabled = true;  
    }
}
