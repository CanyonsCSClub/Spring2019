/*
 * Programmer:   Hunter Goodin 
 * Date Created: 06/06/2019 @  08:20 PM 
 * Last Updated: 08/13/2019 @  05:00 PM by Hunter Goodin
 * File Name:    ControlsMenu.cs 
 * Description:  This script is for the controls menu 
                 This menu doesn't do much, just displays the controls and 
                 has a button to return to main menu. 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsMenu : MonoBehaviour
{
    public AudioSource sound;                   // Menu sfx 

    public void ToMainMenu()
    {
        sound.enabled = true;                   // Play the sfx 
        StartCoroutine(Menu());                 // Start the Menu() coroutine 
    }

    IEnumerator Menu()
    {
        yield return new WaitForSeconds(1.3f);      // Wait 1.3 seconds to allow the sfx to play out 
        Application.LoadLevel("Madian-MainMenu");   // Load the main menu 
    }
}
