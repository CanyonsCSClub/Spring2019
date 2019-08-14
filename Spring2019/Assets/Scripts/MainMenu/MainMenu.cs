/*
 * Programmer:   Madian Jeber & Hunter Goodin
 * Date Created: 06/06/2019 @  08:20 PM 
 * Last Updated: 08/13/2019 @  05:45 PM by Hunter Goodin
 * File Name:    MainMenu.cs 
 * Description:  This script is for controlling the main menu. 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Things are pretty much the same from when Madian coded it but I went ahead and added a sound effect to be played
// when the funtion was called. To let the sound effect play out, I moved everything into coroutines with wait commands. 
// (Hunter) 

public class MainMenu : MonoBehaviour
{
    public GameObject SFXObj;       // The object holding the sound made when an option is chosen 
    public GameObject mouse;        // The mouse obj to be populated in-engine 
    public Animator anim;           // The animator for the mouse to be populated in-engine 
    public GameObject lookAtThis;   // The object the mouse will look at when needed to be populated in-engine 
    private Vector3 lookCoords;     // The coords of the above object 
    private bool animMouse;         // If this bool is toggled, the player chose to start the level 

    private void Start()
    {
        lookCoords = lookAtThis.GetComponent<Transform>().position; // Set lookCoords = to LookAtThis's position 
    }

    private void Update()
    {
        if (animMouse)  //                                                                                  // If animMouse = true... 
        {
            mouse.GetComponent<Transform>().LookAt(lookCoords);                                             // Have the mouseObj face lookCoords 
            anim.Play("RunForwardBattle");                                                                  // Play the running animation 
            mouse.GetComponent<Rigidbody>().transform.Translate(transform.forward * Time.deltaTime * 5);    // Move the mouse forward 
        }
    }

    public void playButton1(string scenename)   // If this function is called by the button... 
    {
        StartCoroutine(RollCredits());          // start the RollCredits() coroutine  
    }

    public void PlayGame()                      // If this function is called by the button... 
    {
        StartCoroutine(PlayGameCo());           // start the PlayGameCo() coroutine 
    }

    public void Controls()                      // If this function is called by the button... 
    {
        StartCoroutine(ControlsCo());           // start the Controlsco() coroutine 
    }

    public void quit()                          // If this function is called by the button...  
    {
        Application.Quit();                     // quit the app 
    }

    IEnumerator ControlsCo()
    {
        SFXObj.GetComponent<MainMenuMusic>().PlayStartSFX();    // Plays the sfx
        yield return new WaitForSeconds(1.3f);                  // Wait 1.3 seconds for the sfx to play out 
        Application.LoadLevel("Hunter-ControlsMenu");           // Load the Controls menu 
    }

    IEnumerator PlayGameCo()  
    {
        SFXObj.GetComponent<MainMenuMusic>().PlayStartSFX();    // Plays the sfx
        animMouse = true;                                       // Set animMouse to true so the mouse will start running 
        yield return new WaitForSeconds(2f);                    // Wait 2 seconds for the sfx to play out 
        Application.LoadLevel("Level1");                        // Load the level 
    }

    IEnumerator RollCredits() 
    {
        SFXObj.GetComponent<MainMenuMusic>().PlayStartSFX();    // Plays the sfx
        yield return new WaitForSeconds(1.3f);                  // Wait 1.3 seconds for the sfx to play out 
        Application.LoadLevel("Credits");                       // Load the credits 
    }
}
