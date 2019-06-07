using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinMenu : MonoBehaviour
{
    public AudioSource sound; 

    public void OnlyButton()
    {
        sound.enabled = true;
        StartCoroutine(RollCredits()); 
    }

    IEnumerator RollCredits()
    {
        yield return new WaitForSeconds(1.3f);
        Application.LoadLevel("Madian-MainMenu");
    }
}
