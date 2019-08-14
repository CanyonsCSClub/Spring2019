/*
 * Programmer:   Hunter Goodin
 * Date Created: 06/06/2019 @  08:20 PM 
 * Last Updated: 08/13/2019 @  05:45 PM by Hunter Goodin
 * File Name:    MainMenuMusic.cs 
 * Description:  This script is for controlling the main menu's music. 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuMusic : MonoBehaviour
{
    public AudioSource music; 		// The main menu song 
    public AudioSource audioSXF;	// The sfx for choosing an option 

    public void PlayStartSFX()
    {
        music.enabled = false; 		// Stop playing the music
        audioSXF.enabled = true;  	// Start playing the sfx 
    }
}
