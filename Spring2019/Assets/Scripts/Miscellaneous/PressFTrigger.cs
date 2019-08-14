/*
 * Programmer:   Hunter Goodin
 * Date Created: 06/06/2019 @  08:10 PM 
 * Last Updated: 08/13/2019 @  06:00 PM by Hunter Goodin
 * File Name:    PressFTrigger.cs 
 * Description:  This script is to  activate the healing area. 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressFTrigger : MonoBehaviour
{
    public bool inside;         // True is the player is inside the area 
    public GameObject fText;    // The text telling the player to press F 
    public GameObject healZone; // The healing zone to activate (inactive by default) 

    void Update ()
    {
        if (inside == true)         // If inside is true... 
        {   
            fText.SetActive(true);  // set fText to active 
            FListener();            // listen for F 
        }
        else if (inside == false)   // If the player is not inside... 
        {
            fText.SetActive(false); // set fText to inactive 
        }
	}

    private void OnTriggerEnter(Collider col)
    {
        if (col.name == "Player")   // If the obj is the player...
        {
            inside = true;          // set inside = to true...
            FListener();            // listen for F
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if(col.name == "Player")    // If the obj is the player...
        {
            inside = false;         // set inside = to false 
        }
        
    }

    private void FListener()
    {
        if (Input.GetKeyDown("f"))      // If the F key is pressed... 
        {
            healZone.SetActive(true);   // activate the healZone obj 
            Destroy(fText);             // destroy the fText obj 
            Destroy(gameObject);        // destroy this obj 
        }
    }
}
