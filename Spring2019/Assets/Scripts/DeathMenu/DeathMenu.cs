/*
 * Programmer:   Hunter Goodin 
 * Date Created: 05/24/2019 @  8:15 PM 
 * Last Updated: 08/13/2019 @  2:20 PM by Hunter Goodin
 * File Name:    DeathMenu.cs 
 * Description:  This script will be responsible for 
 *               the buttons on the death menu.  
 */

 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMenu : MonoBehaviour
{
    public Animator anim;       // The mouse animator to be populated in-engine 
    public AudioSource sound;   // The menu sound to be populated in-engine 
    public AudioSource music;   // The music to be populated in-engine 

	void Start ()           // At the start of the level being loaded... 
    {
        anim.Play("Die");   // Play the death animation for the player 
	}

    public void Retry()                 // When the player clicks the "Retry" button... 
    {
        music.enabled = false;          // turn the music off... 
        sound.enabled = true;           // play the sound effect... 
        StartCoroutine(GetUpMouse());   // and start the GetUpMouse() coroutine 
    }

    public void MainMenu()              // When the player clicks the "Main Menu" button...
    {
        music.enabled = false;          // turn the music off... 
        sound.enabled = true;           // play the sound effect... 
        StartCoroutine(GoToMain());     // and start the GoToMain() coroutine 
    }

    IEnumerator GoToMain()
    {
        yield return new WaitForSeconds(1f);        // Wait one second (so the sound effect can play out)... 
        Application.LoadLevel("Madian-MainMenu");   // load teh main menu 
    }

    IEnumerator GetUpMouse()    
    {
        anim.Play("DieRecover");                // Play the DieRecover animation for the mouse... 
        yield return new WaitForSeconds(1f);    // wait one second (so the animation and sound effect can play out)... 
        Application.LoadLevel("Level1");        // load the level
    }
}
